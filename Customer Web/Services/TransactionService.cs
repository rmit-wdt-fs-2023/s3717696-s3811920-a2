using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using MCBA_Web.Utilities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;


namespace MCBA_Web.Services
{
	public class TransactionService : ITransactionService
	{
        private readonly MCBAContext _context;
        private readonly IBillPayService _billPayService;


        public TransactionService(MCBAContext context, IBillPayService billPayService)
		{
            _context = context;
            _billPayService = billPayService;
        }


        public async Task<IPagedList<Transaction>> GetAccountTransactionsPerPage(int accountNumber, int page = 1)
        {
            const int pageSize = 4;

            var pagedList = await _context.Transaction.Where(x => x.AccountNumber == accountNumber)
                .OrderByDescending(x => x.TransactionTimeUtc)
                .ToPagedListAsync(page, pageSize);

            return pagedList;
        }


        public async Task<IPagedList<BillPay>> GetBillPayTransactionsPerPage(int accountNumber, int page = 1)
        {
            const int pageSize = 4;

            var pagedList = await _context.BillPay.Where(x => x.AccountNumber == accountNumber)
                .OrderByDescending(x => x.ScheduleTimeUtc)
                .ToPagedListAsync(page, pageSize);

            return pagedList;
        }


        public async Task Deposit(DepositViewModel viewModel)
        {
            viewModel.Account.Balance += viewModel.Amount;

            AddTransaction(viewModel.Account, null, TransactionType.Deposit, viewModel.Amount, viewModel.Comment);

            await _context.SaveChangesAsync();
        }


        public async Task Withdraw(WithdrawViewModel viewModel)
        {

            if (FreeTransactionNotAllowed(viewModel.Account.AccountNumber))
                ApplyServiceCharge(viewModel.Account, TransactionType.Withdraw);

            viewModel.Account.Balance -= viewModel.Amount;
            AddTransaction(viewModel.Account, null, TransactionType.Withdraw, viewModel.Amount, viewModel.Comment);

            await _context.SaveChangesAsync();
        }


        public async Task Transfer(TransferViewModel viewModel)
        {

            if (FreeTransactionNotAllowed(viewModel.Account.AccountNumber))
                ApplyServiceCharge(viewModel.Account, TransactionType.Transfer);

            viewModel.Account.Balance -= viewModel.Amount;
            viewModel.DestinationAccount.Balance += viewModel.Amount;

            AddTransaction(viewModel.Account, viewModel.DestinationAccountNumber, TransactionType.Transfer, viewModel.Amount, viewModel.Comment);
            AddTransaction(viewModel.DestinationAccount, null, TransactionType.Transfer, viewModel.Amount, viewModel.Comment);

            await _context.SaveChangesAsync();
        }


        public void AddTransaction(Account account, int? destinationAccountNumber, TransactionType transactionType, decimal amount, string comment)
        {
            Transaction transaction = new Transaction()
            {
                AccountNumber = account.AccountNumber,
                DestinationAccountNumber = destinationAccountNumber,
                TransactionType = transactionType,
                Amount = amount,
                Comment = comment,
                TransactionTimeUtc = DateTime.UtcNow
            };

            _context.Transaction.Add(transaction);
        }


        public void ApplyServiceCharge(Account account, TransactionType transactionType)
        {
            decimal serviceCharge = transactionType.ServiceCharge();
            account.Balance -= serviceCharge;

            Transaction transaction = new Transaction()
            {
                AccountNumber = account.AccountNumber,
                TransactionType = TransactionType.ServiceCharge,
                Amount = serviceCharge,
                Comment = "Applied service charge.",
                TransactionTimeUtc = DateTime.UtcNow
            };

            _context.Transaction.Add(transaction);
        }


        public bool FreeTransactionNotAllowed(int accountNumber)
        {
            bool freeTransactionNotAllowed = false;

            var freeTransactions = _context.Transaction
            .FromSql($"SELECT * FROM [Transaction] WHERE AccountNumber={accountNumber} AND (TransactionType = 'Withdraw' OR (TransactionType = 'Transfer' AND DestinationAccountNumber IS NOT NULL))")
            .Count();

            if (freeTransactions >= 2)
                freeTransactionNotAllowed = true;
            else
                freeTransactionNotAllowed = false;

            return freeTransactionNotAllowed;
        }


        public async Task BillPay(BillPay model)
        {
            // Change local time to utc
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(model.ScheduleTimeUtc, tz);
            model.ScheduleTimeUtc = utcTime;

            await _billPayService.AddNewBillPay(model);
        }


        public async Task RescheduleBillPay(BillPay model)
        {
            // Change local time to utc
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(model.ScheduleTimeUtc, tz);
            model.ScheduleTimeUtc = utcTime;

            await _billPayService.UpdateBillPay(model);
        }


        public async Task CancelScheduledBillPay(int billPayId)
        {
            await _billPayService.DeleteBillPay(billPayId);
        }
    }
}


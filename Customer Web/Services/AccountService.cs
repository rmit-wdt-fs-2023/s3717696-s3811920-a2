using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MCBA_Web.Utilities;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services
{
	public class AccountService : IAccountService
	{
        private readonly MCBAContext _context;


        public AccountService(MCBAContext context)
		{
			_context = context;
		}


        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }


        public async Task<Account> GetById(int? id)
        {
            return await _context.Account.FindAsync(id);
        }


        public Customer GetCustomerById(int id)
        {
            return _context.Customer.Include(x => x.Accounts).FirstOrDefault(x => x.CustomerID == id);
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
            account.Transactions.Add(
                new Transaction
                {
                    DestinationAccountNumber = destinationAccountNumber,
                    TransactionType = transactionType,
                    Amount = amount,
                    Comment = comment,
                    TransactionTimeUtc = DateTime.UtcNow
                });
        }


        public bool FreeTransactionNotAllowed(int accountNumber)
        {
            bool freeTransactionNotAllowed = false;

            var freeTransactions = _context.Transaction
            .FromSql($"SELECT * FROM [Transaction] WHERE AccountNumber={accountNumber} AND (TransactionType = 'Withdraw' OR (TransactionType = 'Transfer' AND DestinationAccountNumber IS NOT NULL))")
            .Count();

            if (freeTransactions > 2)
                freeTransactionNotAllowed = true;
            else
                freeTransactionNotAllowed = false;

            return freeTransactionNotAllowed;
        }


        public void ApplyServiceCharge(Account account, TransactionType transactionType)
        {
            decimal serviceCharge = transactionType.ServiceCharge();
            account.Balance -= serviceCharge;

            account.Transactions.Add(
            new Transaction
            {
                TransactionType = TransactionType.ServiceCharge,
                Amount = serviceCharge,
                Comment = "Applied service charge.",
                TransactionTimeUtc = DateTime.UtcNow
            });
        }
    }
}


using System;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using X.PagedList;

namespace MCBA_Web.Services
{
	public interface ITransactionService
	{
        public Task<IPagedList<Transaction>> GetAccountTransactionsPerPage(int accountNumber, int page = 1);

        public Task<IPagedList<BillPay>> GetBillPayTransactionsPerPage(int accountNumber, int page = 1);

        Task Deposit(DepositViewModel viewModel);

        Task Withdraw(WithdrawViewModel viewModel);

        Task Transfer(TransferViewModel viewModel);

        public void AddTransaction(Account account, int? destinationAccountNumber, TransactionType transactionType, decimal amount, string comment);

        public void ApplyServiceCharge(Account account, TransactionType transactionType);

        bool FreeTransactionNotAllowed(int accountNumber);

        public Task BillPay(BillPay viewModel);

        public Task RescheduleBillPay(BillPay viewModel);

        public Task CancelScheduledBillPay(int billPayId);

    }
}


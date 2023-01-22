using System;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MCBA_Web.Services
{
	public interface IAccountService
	{
        IEnumerable<Account> GetAll();

        Task<Account> GetById(int? id);

        Customer GetCustomerById(int id);

        Task Deposit(DepositViewModel viewModel);

        Task Withdraw(WithdrawViewModel viewModel);

        Task Transfer(TransferViewModel viewModel);

        public void AddTransaction(Account account, int? destinationAccountNumber, TransactionType transactionType, decimal amount, string comment);

        public Task<IPagedList<Transaction>> GetAccountTransactionsPerPage(int accountNumber, int page = 1);

        public Task BillPay(BillPayViewModel viewModel);

        bool FreeTransactionNotAllowed(int accountNumber);

        public void ApplyServiceCharge(Account account, TransactionType transactionType);
    }
}


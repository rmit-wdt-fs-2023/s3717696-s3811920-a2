using System;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MCBA_Web.Services
{
	public interface IAccountService
	{
        Task<Account> GetById(int? id);

        public IEnumerable<Account> GetAllCustomerAccounts(int customerID);

        public IEnumerable<Account> GetAllAccounts();

        public Task CreateNewAccount(Account account);

        public Task UpdateAccountDetails(Account account);

        public Task DeleteAccount(int accountNumber);

    }
}


using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MCBA_Web.Utilities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Xml;

namespace MCBA_Web.Services
{
	public class AccountService : IAccountService
	{
        private readonly MCBAContext _context;


        public AccountService(MCBAContext context)
		{
			_context = context;
		}


        public async Task<Account> GetById(int? id)
        {
            return await _context.Account.FindAsync(id);
        }


        public IEnumerable<Account> GetAllCustomerAccounts(int customerID)
        {
            return _context.Account
                .Include(m => m.Customer)
                .Include(n => n.Transactions)
                .Where(x => x.CustomerID == customerID)
                .ToList();
        }


        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Account
                .Include(m => m.Customer)
                .Include(n => n.Transactions)
                .ToList();
        }


        public async Task CreateNewAccount(Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAccountDetails(Account account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAccount(int accountNumber)
        {
            Account account = await _context.Account.FindAsync(accountNumber);
            _context.Account.Attach(account);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
        }

    }
}


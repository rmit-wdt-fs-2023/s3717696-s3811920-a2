using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services
{
	public class PayeeService : IPayeeService
	{
        private readonly MCBAContext _context;


        public PayeeService(MCBAContext context)
		{
            _context = context;
		}


        public async Task<Payee> GetPayeeById(int id)
        {
            return await _context.Payee.FindAsync(id);
        }


        public IEnumerable<Payee> GetAllPayees()
        {
            return _context.Payee.ToList();
        }


        public async Task CreateNewPayee(Payee payee)
        {
            _context.Payee.Add(payee);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePayeeDetails(Payee payee)
        {
            _context.Payee.Update(payee);
            await _context.SaveChangesAsync();
        }


        public async Task DeletePayee(int payeeId)
        {
            Payee payee = await _context.Payee.FindAsync(payeeId);
            _context.Payee.Attach(payee);
            _context.Payee.Remove(payee);
            await _context.SaveChangesAsync();
        }

    }
}


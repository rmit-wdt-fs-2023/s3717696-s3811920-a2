using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services
{
	public class BillPayService : IBillPayService
	{
        private readonly MCBAContext _context;


        public BillPayService(MCBAContext context)
		{
			_context = context;
		}


        public async Task<BillPay> GetBillById(int id)
        {
            return await _context.BillPay.FindAsync(id);
        }


        public IEnumerable<BillPay> GetBillsByAccountNumber(int accountNumber)
        {
            return _context.BillPay
                .Include(m => m.Account)
                .Include(n => n.Payee)
                .Where(x => x.AccountNumber == accountNumber)
                .ToList();
        }


        public IEnumerable<BillPay> GetBillsByPayeeID(int payeeID)
        {
            return _context.BillPay
                .Include(m => m.Account)
                .Include(n => n.Payee)
                .Where(x => x.PayeeID == payeeID)
                .ToList();
        }


        public async Task AddNewBillPay(BillPay billPay)
        {
            _context.BillPay.Add(billPay);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateBillPay(BillPay billPay)
        {
            _context.BillPay.Update(billPay);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteBillPay(int billPayId)
        {
            BillPay bill = await _context.BillPay.FindAsync(billPayId);
            _context.BillPay.Attach(bill);
            _context.BillPay.Remove(bill);
            await _context.SaveChangesAsync();
        }

    }
}


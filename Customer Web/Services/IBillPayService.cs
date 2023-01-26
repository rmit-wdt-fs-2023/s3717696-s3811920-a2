using System;
using MCBA_Web.Models;

namespace MCBA_Web.Services
{
	public interface IBillPayService
	{
        Task<BillPay> GetBillById(int id);

        public IEnumerable<BillPay> GetBillsByAccountNumber(int accountNumber);

        public IEnumerable<BillPay> GetBillsByPayeeID(int payeeID);

        public Task AddNewBillPay(BillPay billPay);

        public Task UpdateBillPay(BillPay billPay);

        public Task DeleteBillPay(int billPayId);

    }
}


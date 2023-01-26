using System;
using MCBA_Web.Models;

namespace MCBA_Web.Services
{
	public interface IPayeeService
	{
        Task<Payee> GetPayeeById(int id);

        public IEnumerable<Payee> GetAllPayees();

        public Task CreateNewPayee(Payee payee);

        public Task UpdatePayeeDetails(Payee payee);

        public Task DeletePayee(int payeeId);
    }
}


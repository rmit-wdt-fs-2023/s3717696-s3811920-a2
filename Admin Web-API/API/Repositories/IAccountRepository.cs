using MCBA_Admin.Models;

namespace MCBA_Admin.API.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(int id);
    Task<List<Account>> GetAllAsync();

    Task<List<Account>> GetByCustomerIdAsync(int customerId);
}


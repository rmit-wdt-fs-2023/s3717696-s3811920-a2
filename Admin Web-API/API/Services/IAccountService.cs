using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface IAccountService
{
    Task<Account> GetByIdAsync(int id);
    Task<List<Account>> GetAllAsync();
    Task<List<Account>> GetByCustomerIdAsync(int customerId);
}

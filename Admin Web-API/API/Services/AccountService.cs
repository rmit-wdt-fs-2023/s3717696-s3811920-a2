using MCBA_Admin.API.Repositories;
using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        return await _accountRepository.GetByIdAsync(id);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<List<Account>> GetByCustomerIdAsync(int customerId)
    {
        return await _accountRepository.GetByCustomerIdAsync(customerId);
    }
}

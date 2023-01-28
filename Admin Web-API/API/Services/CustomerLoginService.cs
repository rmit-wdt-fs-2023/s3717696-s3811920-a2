using MCBA_Admin.API.Repositories;
using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public class CustomerLoginService : ICustomerLoginService
{
    private readonly ICustomerLoginRepository _customerLoginRepository;

    public CustomerLoginService(ICustomerLoginRepository customerLoginRepository)
    {
        _customerLoginRepository = customerLoginRepository;
    }

    public async Task UpdateLoginDetailsAsync(Login model)
    {
        await _customerLoginRepository.UpdateLoginDetailsAsync(model);

    }

    public async Task<List<Login>> GetAllLoginsAsync()
    {
        return await _customerLoginRepository.GetAllLoginsAsync();
    }

    public async Task<Login> GetLoginByIdAsync(int loginId)
    {
        return await _customerLoginRepository.GetLoginByIdAsync(loginId);
    }
}
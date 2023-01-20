using MCBA_Admin.API.Repositories;
using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.Services;

public class CustomerLoginService : ICustomerLoginService
{
    private readonly ICustomerLoginRepository _customerLoginRepository;

    public CustomerLoginService(ICustomerLoginRepository customerLoginRepository)
    {
        _customerLoginRepository = customerLoginRepository;
    }

    public async Task<bool> UpdateLoginDetailsAsync(int customerId, Login model)
    {
        var result = await _customerLoginRepository.UpdateLoginDetailsAsync(customerId, model);

        if (!result)
        {
            return false;
        }

        return true;
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
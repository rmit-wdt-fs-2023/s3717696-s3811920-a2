using MCBA_Admin.Models;

namespace MCBA_Admin.API.Repositories;

public interface ICustomerLoginRepository
{
    Task<bool> UpdateLoginDetailsAsync(int customerId, Login login);

    Task<List<Login>> GetAllLoginsAsync();

    Task<Login> GetLoginByIdAsync(int loginId);
}


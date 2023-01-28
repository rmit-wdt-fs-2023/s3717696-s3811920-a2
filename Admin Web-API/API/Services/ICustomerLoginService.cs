using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface ICustomerLoginService
{
    Task UpdateLoginDetailsAsync(Login login);

    Task<List<Login>> GetAllLoginsAsync();

    Task<Login> GetLoginByIdAsync(int loginId);
}

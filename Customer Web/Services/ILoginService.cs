using MCBA_Web.Models;

namespace MCBA_Web.Services;

public interface ILoginService
{
    Login AuthenticateCustomer(int loginId, string password);
    Customer GetCustomerByLoginId(int loginId);

    Login GetLoginByCustomerId(int customerId);
    void UpdatePassword(Login login, string newPassword);
    bool IsLocked(int loginId);
}
using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface ILoginService
{
    AdminLogin AuthenticateAdmin(string username, string password);
}
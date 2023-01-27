using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;


namespace MCBA_Admin.Services;

/*
 * This class is specifically for login functionality. All requests 
 * are performed by an API call to the ADMIN-API project.
 *
 * Implements IAdminService
 */
public class LoginService : ILoginService
{
    private readonly MCBAContext _context;

    public LoginService(MCBAContext context)
    {
        _context = context;
    }

    // Authenticates admin. Returns null if admin cannot authenticate.
    public AdminLogin AuthenticateAdmin(string username, string password) 
    {
        if (username == "admin" && password == "password") // HARD CODED
            return new AdminLogin("admin", "password");

        return null;
    }
}
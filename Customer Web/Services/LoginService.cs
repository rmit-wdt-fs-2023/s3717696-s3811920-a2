using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;
using SimpleHashing.Net;

namespace MCBA_Web.Services;

public class LoginService : ILoginService
{
    private readonly MCBAContext _context;
    private readonly ISimpleHash _simpleHash;

    public LoginService(MCBAContext context)
    {
        _context = context;
        _simpleHash = new SimpleHash();
    }

    // Authenticates Customer. Returns null if customer cannot authenticate.
    public Login AuthenticateCustomer(int loginId, string password)
    {
        Login login = _context.Login.Find(loginId);

        if (!_simpleHash.Verify(password, login.PasswordHash))
        {
            return null;
        }

        return login;
    }


    public Customer GetCustomerByLoginId(int loginId)
    {
        return _context.Customer.Find(loginId);
    }
}
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
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

        if (login == null)
            return null;

        if (!_simpleHash.Verify(password, login.PasswordHash))
            return null;

        return login;
    }

    public bool IsLocked(int loginId)
    {
        Login login = _context.Login.Find(loginId);

        if (login.IsLocked)
        {
            return true;
        }

        return false;
    }

    public void UpdatePassword(Login login, string newPassword)
    {
        login.PasswordHash = _simpleHash.Compute(newPassword);

        _context.Login.Update(login);

        _context.SaveChanges();
    }

    public Login GetLoginByCustomerId(int customerId)
    {
        return _context.Login.FirstOrDefault(m => m.CustomerID == customerId);
    }

    public Customer GetCustomerByLoginId(int loginId)
    {
        return _context.Customer.Find(loginId);
    }
}
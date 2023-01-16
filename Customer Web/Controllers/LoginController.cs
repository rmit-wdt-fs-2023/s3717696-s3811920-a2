using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;
using McbaExampleWithLogin.Filters;
using MCBA_Web.Services;

namespace MCBA_Web.Controllers;

[Route("/login")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    // GET: Login
    public IActionResult Index()
    {
        return View("Login");
    }

    [HttpPost]
    public IActionResult Login(int loginID, string password)
    {
        //Console.WriteLine("loginId: " + loginID);
        //Console.WriteLine("password: " + password);

        var login = _loginService.AuthenticateCustomer(loginID, password);

        // Check if authenticated
        if (login == null)
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new Login { LoginID = loginID });
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
        HttpContext.Session.SetString(nameof(Customer.Name), _loginService.GetCustomerByLoginId(login.CustomerID).Name);

        return RedirectToAction("Index", "Customer");
    }

    [Route("/logout")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

}

﻿using Microsoft.AspNetCore.Mvc;
using MCBA_Web.Data;
using MCBA_Web.Models;
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
        var _customerId = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (_customerId.HasValue)
            return RedirectToAction("Index", "Customer", new { customerId = _customerId });

        return View("Login");
    }

    [HttpPost]
    public IActionResult Login(int loginID, string password)
    {

        if (loginID == 0 || password == null)
        {
            return View(new Login { LoginID = loginID });
        }

        var login = _loginService.AuthenticateCustomer(loginID, password);

        // Check if authenticated
        if (login == null)
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new Login { LoginID = loginID });
        }

        // Check if locked
        if (_loginService.IsLocked(login.LoginID))
        {
            ModelState.AddModelError("LoginFailed", "Account locked. Please contact us");
            return View();
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
        HttpContext.Session.SetString(nameof(Customer.Name), _loginService.GetCustomerByLoginId(login.CustomerID).Name);

        return RedirectToAction("Index", "Customer", new { customerId = login.CustomerID});
    }

    [Route("/logout")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

}

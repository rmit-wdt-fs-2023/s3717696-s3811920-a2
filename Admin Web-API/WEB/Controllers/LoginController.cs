using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MCBA_Admin.WEB.Controllers;

[Route("/")]
public class LoginController : Controller
{

    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString(nameof(AdminLogin.Username)) != null)
            return new RedirectResult("/admin/customers");

        return View("WEB/Views/Login/Index.cshtml");
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
		
		if (username == null || password == null){
			ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return RedirectToAction("Index", "Login");
		}
		
        var admin = _loginService.AuthenticateAdmin(username, password);

        // Check if authenticated
        if (admin == null)
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return RedirectToAction("Index", "Login");
        }

        TempData["adminUsername"] = admin.Username;

        // Login admin.
        HttpContext.Session.SetString(nameof(AdminLogin.Username), admin.Username);

        return RedirectToAction("Index", "Admin", admin);
    }

    [Route("/logout")]
    [HttpGet]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Admin");
    }

}

using MCBA_Web.Models;
using MCBA_Web.Services;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCBA_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICustomerService _customerService;
    public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
    {
        _customerService = customerService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var customerId = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (customerId.HasValue)
        {
            var customer = _customerService.GetById((int)customerId);
            return View(customer);
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
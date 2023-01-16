using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;
using McbaExampleWithLogin.Filters;
using MCBA_Web.Services;

namespace MCBA_Web.Controllers;

[AuthorizeCustomer]
[Route("/")] // route to CustomerID
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: Customer
    public IActionResult Index()
    {
        return View("Overview");
    }
}

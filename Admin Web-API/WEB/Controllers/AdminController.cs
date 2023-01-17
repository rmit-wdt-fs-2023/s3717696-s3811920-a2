using MCBA_Admin.Filters;
using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCBA_Admin.WEB.Controllers;


[Route("admin/")]
[AuthorizeAdmin]
public class AdminController : Controller
{
    private readonly ICustomerService _customerService;

    public AdminController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllAsync();

        var adminUsername = TempData["adminUsername"];
        return View("WEB/Views/Admin/Index.cshtml",new { Customers = customers, AdminUsername = adminUsername});
    }

    [HttpGet("manage/{CustomerID}")]
    public async Task<IActionResult> ManageCustomer([FromRoute] int customerId)
    {

        Customer customer = await _customerService.GetByIdAsync(customerId);

        
        return View("WEB/Views/Admin/ManageCustomer.cshtml", customer);
    }

}

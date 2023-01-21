using MCBA_Admin.Filters;
using MCBA_Admin.Models;
using MCBA_Admin.Services;
using MCBA_Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MCBA_Admin.WEB.Controllers;


[Route("Admin/")]
[AuthorizeAdmin]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("customers")]
    public async Task<IActionResult> Index()
    {
        var customers = await _adminService.GetAllCutomersAsync();
        var logins = await _adminService.GetAllCustomerLoginsAsync();

        var viewModel = new CustomerLoginViewModel
        {
            Customers = customers,
            Logins = logins
        };

        return View("WEB/Views/Admin/Customers.cshtml", viewModel);
    }

    [HttpGet("manage/{customerId}")]
    public async Task<IActionResult> ManageCustomer([FromRoute] int customerId)
    {
        if (customerId == null)
            return View("WEB/Views/Admin/Customers.cshtml");

        Customer customer = await _adminService.GetCustomerByIdAsync(customerId);
        List<BillPay> customerBills = await _adminService.GetBillsByCustomerIdAsync(customerId);

        var viewModel = new CustomerBillViewModel
        {
            Customer = customer,
            CustomerAddress = customer.Address,
            CustomerBills = customerBills
        };

        return View("WEB/Views/Admin/ManageCustomer.cshtml", viewModel);//, new { Customer = customer, CustomerBills = customerBills });
    }

    [HttpGet("bills")]
    public async Task<IActionResult> GetBills()
    {

        List<BillPay> bills = await _adminService.GetAllBillPayAsync();

        return View("WEB/Views/Admin/Bills.cshtml", new { Bills = bills });
    }

    [HttpPost("UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer(int customerId, Customer customer)
    {
        var didUpdate = await _adminService.UpdateCustomerAsync(customerId, customer);

        if (!didUpdate)
        {
            ModelState.AddModelError("UpdateFailed", "Update failed.");
            return RedirectToAction("ManageCustomer", "Admin", new { customerId = customer.CustomerID });
        }
        
        return RedirectToAction("ManageCustomer", "Admin", new { customerId = customer.CustomerID});
    }

    [HttpPost("UpdateAddress")]
    public async Task<IActionResult> UpdateAddress(int customerId, Address address)
    {
        address.State = (StateType)Enum.ToObject(typeof(StateType), Convert.ToInt32(Request.Form["CustomerAddress.State"]));

        var didUpdate = await _adminService.UpdateAddressAsync(customerId, address);

        if (!didUpdate)
        {
            ModelState.AddModelError("UpdateFailed", "Update failed.");
        }

        return RedirectToAction("ManageCustomer", "Admin", new { CustomerID = customerId });
    }

    [HttpPost("CreateAddress")]
    public async Task<IActionResult> CreateAddress(int customerId, Address address)
    {
        address.State = (StateType)Enum.ToObject(typeof(StateType), Convert.ToInt32(Request.Form["CustomerAddress.State"]));
        address.Street = Request.Form["CustomerAddress.Street"];
        address.Postcode = Request.Form["CustomerAddress.Postcode"];
        address.City = Request.Form["CustomerAddress.City"];

        var didCreate = await _adminService.CreateAddressAsync(customerId, address);

        if (!didCreate)
        {
            ModelState.AddModelError("CreateFailed", "Create failed.");
        }

        return RedirectToAction("ManageCustomer", "Admin", new { CustomerID = customerId });
    }

    [HttpPost("BlockBillPay")]
    public async Task<IActionResult> BlockBillPay(int customerId, int billPayId)
    {
        
        var bill = await _adminService.GetBillPayByIdAsync(billPayId);

        Console.WriteLine("IN ----------------------------------------------------");

        var isUpdated= await _adminService.BlockBillPayAsync(bill);

        return RedirectToAction("ManageCustomer", "Admin", new { CustomerID = customerId });
    }

    [HttpPost("UnBlockBillPay")]
    public async Task<IActionResult> UnBlockBillPay(int customerId, int billPayId)
    {

        var bill = await _adminService.GetBillPayByIdAsync(billPayId);
        
        var isUpdated = await _adminService.UnBlockBillPayAsync(bill);

        return RedirectToAction("ManageCustomer", "Admin", new { CustomerID = customerId });
    }

    [HttpPost("LockCustomerLogin")]
    public async Task<IActionResult> LockCustomerLogin(int customerId, int loginId)
    {

        var login = await _adminService.GetCustomerLoginByIdAsync(loginId);

        var isUpdated = await _adminService.LockCustomerAsync(login);

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost("UnLockCustomerLogin")]
    public async Task<IActionResult> UnLockCustomerLogin(int customerId, int loginId)
    {

        var login = await _adminService.GetCustomerLoginByIdAsync(loginId);

        var isUpdated = await _adminService.UnLockCustomerAsync(login);

        return RedirectToAction("Index", "Admin");
    }

}

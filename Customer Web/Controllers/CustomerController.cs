﻿using Microsoft.AspNetCore.Mvc;
using McbaExampleWithLogin.Filters;
using MCBA_Web.Services;
using MCBA_Web.Models;

namespace MCBA_Web.Controllers;

[AuthorizeCustomer]
[Route("MyProfile")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // GET: Customer
    [Route("{customerId}")]
    public IActionResult Index(int customerId)
    {
        var customer = _customerService.GetById(customerId);

        return View("Overview", customer);
    }

    [HttpPost("UpdateCustomer")]
    public IActionResult UpdateCustomer(int _customerId, Customer customer)
    {
        _customerService.Update(customer);

        return RedirectToAction("Index", "Customer", new { customerId = customer.CustomerID });
    }

    [HttpPost("UploadImage")]
    public IActionResult UploadImage(Customer customer)
    {

        if (customer.ImageUpload != null && customer.ImageUpload.Length > 0)
        {
            _customerService.UpdateProfilePicture(customer);
        }

        return RedirectToAction("Index", "Customer", new {customerId = customer.CustomerID});
    }

    [HttpGet("GetProfilePicture/{customerId}")]
    public IActionResult GetProfilePicture(int customerId)
    {
        var customer = _customerService.GetById(customerId);

        return File(customer.ProfilePicture, customer.ProfilePictureContentType);
    }
}

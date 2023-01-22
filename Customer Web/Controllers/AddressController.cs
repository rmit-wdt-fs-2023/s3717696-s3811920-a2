using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.Services;

namespace MCBA_Web.Controllers;

[Route("/[controller]")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }


    [HttpPost("UpdateAddress")]
    public IActionResult UpdateAddress(int _customerId, Address address)
    {
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine(_customerId);

        _addressService.Update(_customerId, address);

        return RedirectToAction("Index", "Customer", new { customerId = _customerId });
    }

    [HttpPost("CreateAddress")]
    public IActionResult CreateAddress(int customerId, Address address)
    {
        Console.WriteLine("--------asdfasdfasd---------------------------------------------------");
        //address.State = (StateType)Enum.ToObject(typeof(StateType), Convert.ToInt32(Request.Form["CustomerAddress.State"]));
        //address.Street = Request.Form["CustomerAddress.Street"];
        //address.Postcode = Request.Form["CustomerAddress.Postcode"];
        //address.City = Request.Form["CustomerAddress.City"];

        _addressService.Add(customerId, address);

        return RedirectToAction("Index", "Customer", new { CustomerID = customerId });
    }
}

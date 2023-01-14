using MCBA_Web.Models;
using MCBA_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web.Controllers;

public class CustomerController : Controller
{

    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {

        _customerService = customerService;
    }

    public ActionResult Index()
    {
        var customers = _customerService.GetAll();
        return View(customers);
    }

    public ActionResult Details(int id)
    {
        var customer = _customerService.GetById(id);
        return View(customer);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Customer customer)
    {
        //if (ModelState.IsValid)
        //{
        _customerService.Add(customer);
        return RedirectToAction("Index");
        //}

        return View(customer);
    }

    public ActionResult Edit(int id)
    {
        var customer = _customerService.GetById(id);
        return View(customer);
    }

    [HttpPost]
    public ActionResult Edit(Customer customer)
    {
        if (ModelState.IsValid)
        {
            _customerService.Update(customer);
            _customerService.Save();
            return RedirectToAction("Index");
        }
        return View(customer);
    }
}
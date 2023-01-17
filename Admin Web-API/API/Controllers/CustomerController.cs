using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<Customer> Post([FromBody] Customer customer)
    {
        _customerService.Add(customer);
        return CreatedAtAction("Get", new { id = customer.CustomerID }, customer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return customer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();

        if (customers.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(customers);
    }

    [HttpPut("{id}")]
    public IActionResult ManageCustomer(int id, [FromBody] Customer updatedCustomer)
    {

        //var customer = JsonConvert.DeserializeObject<Customer>(updatedCustomer);
        //Console.WriteLine(customer.Mobile);


        // Save
        _customerService.Update(id, updatedCustomer);


        return Ok();
    }
}
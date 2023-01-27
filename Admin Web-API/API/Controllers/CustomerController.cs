using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // POST: Creates new Customer from passed model 
    [HttpPost]
    public ActionResult<Customer> Post(int id, [FromBody] Customer customer)
    {
        if (!ModelState.IsValid) // check if model is valid
            return BadRequest(customer);

        _customerService.Add(customer); // add new customer

        return CreatedAtAction("Get", new { id = customer.CustomerID }, customer);
    }

    // GET: Returns Customer by id
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(int id)
    {
        var customer = await _customerService.GetByIdAsync(id); // get customer
        
        if (customer == null)
            return NotFound();

        return customer;
    }

    // GET: Returns All Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();

        if (!customers.Any())
            return NotFound();


        return Ok(customers);
    }

    // PUT: Updates Customer by id
    [HttpPut("{id}")]
    public IActionResult ManageCustomer(int id, [FromBody] Customer updatedCustomer)
    {
        if (!ModelState.IsValid) // check if model is valid
            return BadRequest(updatedCustomer);

        _customerService.Update(id, updatedCustomer); // update
        

        return Ok(updatedCustomer);
    }
}
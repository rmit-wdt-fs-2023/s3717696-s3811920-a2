using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddressController : ControllerBase
{

    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    // POST: Creates an address from passed Address model.
    [HttpPost]
    public ActionResult<Address> Post([FromBody] Address address)
    {
        if (!ModelState.IsValid) // check if passed address is valid
            return BadRequest(ModelState);

        _addressService.Add(address); // add new address

        return CreatedAtAction("Get", new { id = address.CustomerID }, address);
    }

    // PUT: Updates address from passed Address model and address Id.
    [HttpPut("{id}")]
    public ActionResult<Address> Put(int id, [FromBody] Address address)
    {
        if (!ModelState.IsValid) // check if passed address is valid
            return BadRequest(address);

        _addressService.Update(id, address); // update address

        return CreatedAtAction("Get", new { id = address.CustomerID }, address);
    }

    // GET: Gets address from passed address Id.
    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> Get(int id)
    {
        var address = await _addressService.GetByIdAsync(id); // get address

        if (address == null) // check if address exists
        {
            return NotFound();
        }

        return address;
    }

    // GET: Gets all addresses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAllAsync()
    {
        var addresses = await _addressService.GetAllAsync(); // get addresses

        if (!addresses.Any()) // check if adresses are emtpy
        {
            return NotFound();
        }

        return Ok(addresses);
    }
}
using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddressController : ControllerBase
{

    private readonly ILogger<AddressController> _logger;
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService, ILogger<AddressController> logger)
    {
        _addressService = addressService;
        _logger = logger;
    }

    [HttpPost()]
    public ActionResult<Address> Post([FromBody] Address address)
    {
        _addressService.Add(address);

        return CreatedAtAction("Get", new { id = address.CustomerID }, address);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> Get(int id)
    {
        var address = await _addressService.GetByIdAsync(id);
        if (address == null)
        {
            return NotFound();
        }
        return address;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAllAsync()
    {
        var address = await _addressService.GetAllAsync();

        if (!address.Any())
        {
            return NotFound();
        }

        return Ok(address);
    }
}
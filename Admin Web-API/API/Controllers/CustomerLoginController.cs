using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerLoginController : ControllerBase
{
    private readonly ICustomerLoginService _customerLoginService;

    public CustomerLoginController(ICustomerLoginService customerLoginService)
    {
        _customerLoginService = customerLoginService;
    }

    // GET: Returns All Logins
    [HttpGet]
    public async Task<ActionResult<List<Login>>> GetAllAsync()
    {
        var logins = await _customerLoginService.GetAllLoginsAsync();

        if (!logins.Any())
        {
            return NotFound();
        }

        return Ok(logins);
    }

    // GET: Returns Login by passed id
    [HttpGet("{loginId}")]
    public async Task<ActionResult<Login>> GetLoginByIdAsync(int loginId)
    {
        var login = await _customerLoginService.GetLoginByIdAsync(loginId);

        if (login == null)
        {
            return NotFound();
        }

        return Ok(login);
    }

    // PUT: Updated Login by passed id and new Login
    [HttpPut("{loginId}")]
    public async Task<IActionResult> UpdateLoginDetails(Login login)
    {

        if (!ModelState.IsValid) // check if model is valid
            return BadRequest(login);

        await _customerLoginService.UpdateLoginDetailsAsync(login); // update

        return Ok();
    }
}

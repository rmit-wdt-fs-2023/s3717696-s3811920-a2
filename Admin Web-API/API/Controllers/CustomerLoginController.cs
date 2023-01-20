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

    [HttpPut("{loginId}")]
    public async Task<IActionResult> UpdateLoginDetails(int loginId, Login login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _customerLoginService.UpdateLoginDetailsAsync(loginId, login);

        return Ok();
    }
}

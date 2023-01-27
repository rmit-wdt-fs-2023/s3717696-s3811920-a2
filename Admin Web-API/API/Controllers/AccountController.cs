using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET: Returns all the accounts for the passed customer id.
    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetAccountsByCustomerID(int id)
    {
        var accounts = await _accountService.GetByCustomerIdAsync(id);

        if (accounts.IsNullOrEmpty())
            return NotFound();

        return Ok(accounts);
    }

    // GET: Returns all accounts 
    [HttpGet("accounts")]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}

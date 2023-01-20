using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetAccountsByCustomerID(int id)
    {
        var accounts = await _accountService.GetByCustomerIdAsync(id);
        return Ok(accounts);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}

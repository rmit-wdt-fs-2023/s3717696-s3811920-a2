using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BillPayController : ControllerBase
{

    private readonly ILogger<BillPayController> _logger;
    private readonly IBillPayService _billPayService;

    public BillPayController(IBillPayService billPayService, ILogger<BillPayController> logger)
    {
        _billPayService = billPayService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var billPay = await _billPayService.GetByIdAsync(id);
        if (billPay == null)
            return NotFound();
        return Ok(billPay);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var billPays = await _billPayService.GetAllAsync();
        return Ok(billPays);
    }

    [HttpPut("{id}")]
    public ActionResult<Customer> Put(int id, [FromBody] BillPay bill)
    {
        _billPayService.Update(id, bill);
        return CreatedAtAction("Get", new { id = bill.BillPayID }, bill);
    }
}
using MCBA_Admin.Models;
using MCBA_Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Admin.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BillPayController : ControllerBase
{
    private readonly IBillPayService _billPayService;

    public BillPayController(IBillPayService billPayService)
    {
        _billPayService = billPayService;
    }

    // GET: returns BillPay from passed Id.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var billPay = await _billPayService.GetByIdAsync(id);
        if (billPay == null)
            return NotFound();
        return Ok(billPay);
    }

    // GET: returns all BillPays.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var billPays = await _billPayService.GetAllAsync();
        return Ok(billPays);
    }

    // PUT: Updates bill from passed BillPay model and its id.
    [HttpPut("{id}")]
    public ActionResult<BillPay> Put(int id, BillPay bill)
    {
        if (!ModelState.IsValid) // Check if bill is valid
            return BadRequest(bill);

        _billPayService.Update(id, bill); // Update billPay

        return CreatedAtAction("Get", new { id = bill.BillPayID }, bill);
    }
}
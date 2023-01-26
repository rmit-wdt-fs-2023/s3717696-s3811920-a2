using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.Services;
namespace MCBA_Web.Controllers;
using MCBA_Web.Utilities;
using X.PagedList;


[Route("Account")]
public class BillPayController : Controller
{
    private readonly IAccountService _accountService;

    private readonly ITransactionService _transactionService;

    private readonly IPayeeService _payeeService;

    private readonly IBillPayService _billPayService;


    public BillPayController(IAccountService accountService, ITransactionService transactionService,
        IPayeeService payeeService, IBillPayService billPayService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _payeeService = payeeService;
        _billPayService = billPayService;
    }


    // Get: Account/ScheduledBills
    [HttpGet("Bills/{accountNumber}")]
    public async Task<IActionResult> ScheduledBills(int accountNumber, int page = 1)
    {
        var account = await _accountService.GetById(accountNumber);
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (account is null || account.IsNotOwned(customerID))
            return RedirectToAction(nameof(Index), "Customer", new { customerID });

        ViewBag.Account = account;

        IPagedList<BillPay> pagedList = await _transactionService.GetBillPayTransactionsPerPage(account.AccountNumber, page);

        return View(pagedList);
    }


    // GET: Account/ViewBillPay
    [HttpPost("ViewBillPay")]
    public IActionResult ViewBillPay(BillPay model)
    {
        return View(model);
    }


    // POST: Account/BillPay
    [HttpPost("BillPay")]
    public async Task<IActionResult> BillPay(int accountNumber, BillPay model)
    {

        if (model.Amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(model.Amount), "Amount cannot have more than 2 decimal places.");

        var payee = await _payeeService.GetPayeeById(model.PayeeID);

        if (payee == null)
            ModelState.AddModelError(nameof(model.PayeeID), "Payee doesn't exist.");

        if (!ModelState.IsValid)
            return View("ViewBillPay", model);

        await _transactionService.BillPay(model);

        var account = await _accountService.GetById(accountNumber);
        return RedirectToAction(nameof(Index), "Customer", new { customerId = account.CustomerID });
    }


    // GET: Account/RetryBillPay
    [HttpPost("RetryBillPay")]
    public async Task<IActionResult> RetryBillPay(int accountNumber, int billPayId)
    {
        var account = await _accountService.GetById(accountNumber);
        if (account is null)
            return RedirectToAction(nameof(Index));
        ViewBag.Account = account;

        BillPay bill = await _billPayService.GetBillById(billPayId);

        return View(bill);
    }


    // POST: Account/EditBillPay
    [HttpPost("EditBillPay")]
    public async Task<IActionResult> EditBillPay(BillPay model)
    {
        var account = await _accountService.GetById(model.AccountNumber);
        await _transactionService.RescheduleBillPay(model);
        return RedirectToAction(nameof(Index), "Customer", new { account.CustomerID });
    }


    // POST: Account/CancelBillPay
    [HttpPost("CancelBillPay")]
    public async Task<IActionResult> CancelBillPay(int accountNumber, int billPayId)
    {
        await _transactionService.CancelScheduledBillPay(billPayId);
        return RedirectToAction(nameof(ScheduledBills), new { accountNumber = accountNumber });
    }

}

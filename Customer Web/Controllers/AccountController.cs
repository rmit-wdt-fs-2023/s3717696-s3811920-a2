using Microsoft.AspNetCore.Mvc;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using MCBA_Web.Utilities;
using MCBA_Web.Services;
using X.PagedList;

namespace MCBA_Web.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET: Account/Deposit?accountNumber=4100
    [HttpGet("Deposit/{accountNumber}")]
    public async Task<IActionResult> Deposit(int accountNumber)
    {
        var account = await _accountService.GetById(accountNumber);
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (account is null || account.IsNotOwned(customerID))
            return RedirectToAction(nameof(Index), "Customer", new { customerID });

        return View("DepositForm",
            new DepositViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)

            });
    }

    // POST: Account/Deposit
    [HttpPost("Deposit/{accountNumber}")]
    public async Task<IActionResult> Deposit(int customerId, DepositViewModel viewModel)
    {
        viewModel.Account = await _accountService.GetById(viewModel.AccountNumber);

        if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
        {
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
            return View(viewModel);
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        await _accountService.Deposit(viewModel);

        return RedirectToAction(nameof(Index), "Customer", new { customerId });
    }


    // GET: Account/Withdraw?accountNumber=4100
    [HttpGet("Withdraw/{accountNumber}")]
    public async Task<IActionResult> Withdraw(int accountNumber)
    {
        var account = await _accountService.GetById(accountNumber);
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (account is null || account.IsNotOwned(customerID))
            return RedirectToAction(nameof(Index), "Customer", new { customerID });

        return View("WithdrawForm",
            new WithdrawViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)
            });
    }


    // POST: Account/Withdraw
    [HttpPost("Withdraw/{accountNumber}")]
    public async Task<IActionResult> Withdraw(int customerId, WithdrawViewModel viewModel)
    {

        viewModel.Account = await _accountService.GetById(viewModel.AccountNumber);

        if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");

        if (viewModel.Account.HasInsufficientBalance(viewModel.Amount, viewModel.Account.AccountType))
            ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient balance.");

        if (!ModelState.IsValid)
            return View(viewModel);

        await _accountService.Withdraw(viewModel);

        return RedirectToAction(nameof(Index), "Customer", new { customerId });
    }


    // GET: Account/Transfer?accountNumber=4100
    [HttpGet("Transfer/{accountNumber}")]
    public async Task<IActionResult> Transfer(int accountNumber)
    {
        var account = await _accountService.GetById(accountNumber);
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (account is null || account.IsNotOwned(customerID))
            return RedirectToAction(nameof(Index), "Customer", new { customerID });

        return View("TransferForm",
            new TransferViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)
            });
    }


    // POST: Account/Transfer
    [HttpPost("Transfer/{accountNumber}")]
    public async Task<IActionResult> Transfer(int customerId, TransferViewModel viewModel)
    {
        viewModel.Account = await _accountService.GetById(viewModel.AccountNumber);
        viewModel.DestinationAccount = await _accountService.GetById(viewModel.DestinationAccountNumber);

        if (await _accountService.GetById(viewModel.DestinationAccountNumber) == null
            || viewModel.AccountNumber.Equals(viewModel.DestinationAccountNumber))
            ModelState.AddModelError(nameof(viewModel.DestinationAccountNumber), "Invalid destination account number.");

        if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");

        if (viewModel.Account.HasInsufficientBalance(viewModel.Amount, viewModel.Account.AccountType))
            ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient balance.");

        if (!ModelState.IsValid)
            return View("TransferForm",viewModel);

        await _accountService.Transfer(viewModel);

        return RedirectToAction(nameof(Index), "Customer", new { customerId });

    }


    // Get: Account/MyStatements
    [HttpGet("MyStatements/{accountNumber}")]
    public async Task<IActionResult> MyStatements(int accountNumber, int page = 1)
    {
        var account = await _accountService.GetById(accountNumber);
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));

        if (account is null || account.IsNotOwned(customerID))
            return RedirectToAction(nameof(Index), "Customer", new { customerID });

        ViewBag.Account = account;
        IPagedList<Transaction> pagedList = await _accountService.GetAccountTransactionsPerPage(account.AccountNumber, page);

        return View(pagedList);
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

        IPagedList<BillPay> pagedList = await _accountService.GetBillPayTransactionsPerPage(account.AccountNumber, page);

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

        var payee = await _accountService.GetPayeeById(model.PayeeID);

        if (payee == null)
            ModelState.AddModelError(nameof(model.PayeeID), "Payee doesn't exist.");

        if (!ModelState.IsValid)
            return View("ViewBillPay", model);

        var acc = await _accountService.GetById(accountNumber);
        await _accountService.BillPay(model);
        return RedirectToAction(nameof(Index), "Customer", new { customerId = acc.CustomerID });
    }


    // POST: Account/EditBillPay
    [HttpPost("EditBillPay")]
    public async Task<IActionResult> EditBillPay(BillPay model)
    {
        var account = await _accountService.GetById(model.AccountNumber);
        await _accountService.RescheduleBillPay(model);
        return RedirectToAction(nameof(Index), "Customer", new { account.CustomerID });
    }


    // GET: Account/RetryBillPay
    [HttpPost("RetryBillPay")]
    public async Task<IActionResult> RetryBillPay(int accountNumber, int billPayId)
    {
        var account = await _accountService.GetById(accountNumber);
        if (account is null)
            return RedirectToAction(nameof(Index));
        ViewBag.Account = account;

        BillPay bill = await _accountService.GetBillById(billPayId);
        
        return View(bill);
    }


    // POST: Account/CancelBillPay
    [HttpPost("CancelBillPay")]
    public async Task<IActionResult> CancelBillPay(int accountNumber, int billPayId)
    {
        await _accountService.CancelScheduledBillPay(billPayId);
        return RedirectToAction(nameof(ScheduledBills), new { accountNumber = accountNumber });
    }
}

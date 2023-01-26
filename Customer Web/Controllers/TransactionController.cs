using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using MCBA_Web.Services;
using MCBA_Web.Utilities;
using X.PagedList;

namespace MCBA_Web.Controllers;

[Route("Account")]
public class TransactionController : Controller
{
    private readonly IAccountService _accountService;

    private readonly ITransactionService _transactionService;


    public TransactionController(IAccountService accountService, ITransactionService transactionService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
    }


    // GET: Account/Deposit?accountNumber=4100
    [HttpGet("Deposit/{accountNumber}")]
    public async Task<IActionResult> Deposit(int accountNumber)
    {
        var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
        var account = await _accountService.GetById(accountNumber);

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
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");

        if (!ModelState.IsValid)
            return View("DepositForm",
                new DepositViewModel
                {
                    AccountNumber = viewModel.AccountNumber,
                    Account = viewModel.Account
                });


        await _transactionService.Deposit(viewModel);

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
            return View("WithdrawForm",
                new WithdrawViewModel
                {
                    AccountNumber = viewModel.AccountNumber,
                    Account = viewModel.Account
                });

        await _transactionService.Withdraw(viewModel);

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
            return View("TransferForm",
                new TransferViewModel
                {
                    AccountNumber = viewModel.AccountNumber,
                    Account = viewModel.Account
                });


        await _transactionService.Transfer(viewModel);

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
        IPagedList<Transaction> pagedList = await _transactionService.GetAccountTransactionsPerPage(account.AccountNumber, page);

        return View(pagedList);
    }

}

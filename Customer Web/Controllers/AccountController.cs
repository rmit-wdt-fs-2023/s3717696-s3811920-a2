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
using MCBA_Web.Utilities;
using MCBA_Web.Services;

namespace MCBA_Web.Controllers;

//[Route("/account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly int _customerID = 2100;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET: Account
    public async Task<IActionResult> Index()
    {
        var customer = _accountService.GetCustomerById(_customerID);
        return View(customer);
    }


    // GET: Account/Deposit?accountNumber=4100
    public async Task<IActionResult> Deposit(int accountNumber)
    {
        return View(
            new DepositViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)

            });
    }

    // POST: Account/Deposit
    [HttpPost]
    public async Task<IActionResult> Deposit(DepositViewModel viewModel)
    {
        viewModel.Account = await _accountService.GetById(viewModel.AccountNumber);

        if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");

        if (!ModelState.IsValid)
            return View(viewModel);

        await _accountService.Deposit(viewModel);

        return RedirectToAction(nameof(Index));
    }


    // GET: Account/Withdraw?accountNumber=4100
    public async Task<IActionResult> Withdraw(int accountNumber)
    {
        return View(
            new WithdrawViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)
            });
    }


    // POST: Account/Withdraw
    [HttpPost]
    public async Task<IActionResult> Withdraw(WithdrawViewModel viewModel)
    {
        viewModel.Account = await _accountService.GetById(viewModel.AccountNumber);

        if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");

        if (viewModel.Account.HasInsufficientBalance(viewModel.Amount, viewModel.Account.AccountType))
            ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient balance.");

        if (!ModelState.IsValid)
            return View(viewModel);

        await _accountService.Withdraw(viewModel);

        return RedirectToAction(nameof(Index));
    }


    // GET: Account/Transfer?accountNumber=4100
    public async Task<IActionResult> Transfer(int accountNumber)
    {
        return View(
            new TransferViewModel
            {
                AccountNumber = accountNumber,
                Account = await _accountService.GetById(accountNumber)
            });
    }


    // POST: Account/Transfer
    [HttpPost]
    public async Task<IActionResult> Transfer(TransferViewModel viewModel)
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
            return View(viewModel);

        await _accountService.Transfer(viewModel);

        return RedirectToAction(nameof(Index));

    }
}

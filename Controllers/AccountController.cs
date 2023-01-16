﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;

namespace MCBA_Web.Controllers;

[Route("/account")]
public class AccountController : Controller
{
    private readonly MCBAContext _context;

    public AccountController(MCBAContext context)
    {
        _context = context;
    }

    // GET: Accounts
    public async Task<IActionResult> Index()
    {
        var mCBAContext = _context.Account.Include(a => a.Customer);
        return View(await mCBAContext.ToListAsync());
    }

    // GET: Accounts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Account == null)
        {
            return NotFound();
        }

        var account = await _context.Account
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(m => m.AccountNumber == id);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }

    // GET: Accounts/Create
    public IActionResult Create()
    {
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
        return View();
    }

    // POST: Accounts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AccountNumber,AccountType,CustomerID")] Account account)
    {
        if (ModelState.IsValid)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", account.CustomerID);
        return View(account);
    }

    // GET: Accounts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Account == null)
        {
            return NotFound();
        }

        var account = await _context.Account.FindAsync(id);
        if (account == null)
        {
            return NotFound();
        }
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", account.CustomerID);
        return View(account);
    }

    // POST: Accounts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AccountNumber,AccountType,CustomerID")] Account account)
    {
        if (id != account.AccountNumber)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.AccountNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", account.CustomerID);
        return View(account);
    }

    // GET: Accounts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Account == null)
        {
            return NotFound();
        }

        var account = await _context.Account
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(m => m.AccountNumber == id);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }

    // POST: Accounts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Account == null)
        {
            return Problem("Entity set 'MCBAContext.Account'  is null.");
        }
        var account = await _context.Account.FindAsync(id);
        if (account != null)
        {
            _context.Account.Remove(account);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AccountExists(int id)
    {
        return _context.Account.Any(e => e.AccountNumber == id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCBA_Web.Data;
using MCBA_Web.Models;

namespace MCBA_Web.Controllers;

public class TransactionController : Controller
{
    private readonly MCBAContext _context;

    public TransactionController(MCBAContext context)
    {
        _context = context;
    }

    // GET: Transaction
    public async Task<IActionResult> Index()
    {
        var mCBAContext = _context.Transaction.Include(t => t.Account);
        return View(await mCBAContext.ToListAsync());
    }

    // GET: Transaction/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Transaction == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transaction
            .Include(t => t.Account)
            .FirstOrDefaultAsync(m => m.TransactionID == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transaction/Create
    public IActionResult Create()
    {
        ViewData["AccountNumber"] = new SelectList(_context.Account, "AccountNumber", "AccountNumber");
        return View();
    }

    // POST: Transaction/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TransactionID,TransactionType,AccountNumber,DestinationAccountNumber,Amount,Comment,TransactionTimeUtc")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AccountNumber"] = new SelectList(_context.Account, "AccountNumber", "AccountNumber", transaction.AccountNumber);
        return View(transaction);
    }

    // GET: Transaction/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Transaction == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transaction.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        ViewData["AccountNumber"] = new SelectList(_context.Account, "AccountNumber", "AccountNumber", transaction.AccountNumber);
        return View(transaction);
    }

    // POST: Transaction/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TransactionID,TransactionType,AccountNumber,DestinationAccountNumber,Amount,Comment,TransactionTimeUtc")] Transaction transaction)
    {
        if (id != transaction.TransactionID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.TransactionID))
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
        ViewData["AccountNumber"] = new SelectList(_context.Account, "AccountNumber", "AccountNumber", transaction.AccountNumber);
        return View(transaction);
    }

    // GET: Transaction/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Transaction == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transaction
            .Include(t => t.Account)
            .FirstOrDefaultAsync(m => m.TransactionID == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Transaction == null)
        {
            return Problem("Entity set 'MCBAContext.Transaction'  is null.");
        }
        var transaction = await _context.Transaction.FindAsync(id);
        if (transaction != null)
        {
            _context.Transaction.Remove(transaction);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionExists(int id)
    {
        return _context.Transaction.Any(e => e.TransactionID == id);
    }
}

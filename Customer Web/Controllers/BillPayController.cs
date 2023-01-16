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

public class BillPayController : Controller
{
    private readonly MCBAContext _context;

    public BillPayController(MCBAContext context)
    {
        _context = context;
    }

    // GET: BillPay
    public async Task<IActionResult> Index()
    {
        var mCBAContext = _context.BillPay.Include(b => b.Payee);
        return View(await mCBAContext.ToListAsync());
    }

    // GET: BillPay/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.BillPay == null)
        {
            return NotFound();
        }

        var billPay = await _context.BillPay
            .Include(b => b.Payee)
            .FirstOrDefaultAsync(m => m.BillPayID == id);
        if (billPay == null)
        {
            return NotFound();
        }

        return View(billPay);
    }

    // GET: BillPay/Create
    public IActionResult Create()
    {
        ViewData["PayeeID"] = new SelectList(_context.Payee, "PayeeID", "Name");
        return View();
    }

    // POST: BillPay/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleTimeUtc,PaymentPeriod")] BillPay billPay)
    {
        if (ModelState.IsValid)
        {
            _context.Add(billPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PayeeID"] = new SelectList(_context.Payee, "PayeeID", "Name", billPay.PayeeID);
        return View(billPay);
    }

    // GET: BillPay/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.BillPay == null)
        {
            return NotFound();
        }

        var billPay = await _context.BillPay.FindAsync(id);
        if (billPay == null)
        {
            return NotFound();
        }
        ViewData["PayeeID"] = new SelectList(_context.Payee, "PayeeID", "Name", billPay.PayeeID);
        return View(billPay);
    }

    // POST: BillPay/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleTimeUtc,PaymentPeriod")] BillPay billPay)
    {
        if (id != billPay.BillPayID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(billPay);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillPayExists(billPay.BillPayID))
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
        ViewData["PayeeID"] = new SelectList(_context.Payee, "PayeeID", "Name", billPay.PayeeID);
        return View(billPay);
    }

    // GET: BillPay/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.BillPay == null)
        {
            return NotFound();
        }

        var billPay = await _context.BillPay
            .Include(b => b.Payee)
            .FirstOrDefaultAsync(m => m.BillPayID == id);
        if (billPay == null)
        {
            return NotFound();
        }

        return View(billPay);
    }

    // POST: BillPay/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.BillPay == null)
        {
            return Problem("Entity set 'MCBAContext.BillPay'  is null.");
        }
        var billPay = await _context.BillPay.FindAsync(id);
        if (billPay != null)
        {
            _context.BillPay.Remove(billPay);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BillPayExists(int id)
    {
        return _context.BillPay.Any(e => e.BillPayID == id);
    }
}

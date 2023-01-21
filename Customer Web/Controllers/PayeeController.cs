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

public class PayeeController : Controller
{
    private readonly MCBAContext _context;

    public PayeeController(MCBAContext context)
    {
        _context = context;
    }

    //// GET: Payee
    //public async Task<IActionResult> Index()
    //{
    //    var mCBAContext = _context.Payee.Include(p => p.Address);
    //    return View(await mCBAContext.ToListAsync());
    //}

    //// GET: Payee/Details/5
    //public async Task<IActionResult> Details(int? id)
    //{
    //    if (id == null || _context.Payee == null)
    //    {
    //        return NotFound();
    //    }

    //    var payee = await _context.Payee
    //        .Include(p => p.Address)
    //        .FirstOrDefaultAsync(m => m.PayeeID == id);
    //    if (payee == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(payee);
    //}

    //// GET: Payee/Create
    //public IActionResult Create()
    //{
    //    ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "AddressID");
    //    return View();
    //}

    //// POST: Payee/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("PayeeID,Name,Phone,AddressID")] Payee payee)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(payee);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "AddressID", payee.AddressID);
    //    return View(payee);
    //}

    //// GET: Payee/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null || _context.Payee == null)
    //    {
    //        return NotFound();
    //    }

    //    var payee = await _context.Payee.FindAsync(id);
    //    if (payee == null)
    //    {
    //        return NotFound();
    //    }
    //    ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "AddressID", payee.AddressID);
    //    return View(payee);
    //}

    //// POST: Payee/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("PayeeID,Name,Phone,AddressID")] Payee payee)
    //{
    //    if (id != payee.PayeeID)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(payee);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!PayeeExists(payee.PayeeID))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "AddressID", payee.AddressID);
    //    return View(payee);
    //}

    //// GET: Payee/Delete/5
    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null || _context.Payee == null)
    //    {
    //        return NotFound();
    //    }

    //    var payee = await _context.Payee
    //        .Include(p => p.Address)
    //        .FirstOrDefaultAsync(m => m.PayeeID == id);
    //    if (payee == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(payee);
    //}

    //// POST: Payee/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    if (_context.Payee == null)
    //    {
    //        return Problem("Entity set 'MCBAContext.Payee'  is null.");
    //    }
    //    var payee = await _context.Payee.FindAsync(id);
    //    if (payee != null)
    //    {
    //        _context.Payee.Remove(payee);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool PayeeExists(int id)
    //{
    //    return _context.Payee.Any(e => e.PayeeID == id);
    //}
}

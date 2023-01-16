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

[Route("/profile/MyDetails")]
public class AddressController : Controller
{
    private readonly MCBAContext _context;

    public AddressController(MCBAContext context)
    {
        _context = context;
    }

    // GET: Address
    public async Task<IActionResult> Index()
    {
        var mCBAContext = _context.Address.Include(a => a.Customer);
        return View(await mCBAContext.ToListAsync());
    }

    // GET: Address/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Address == null)
        {
            return NotFound();
        }

        var address = await _context.Address
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(m => m.AddressID == id);
        if (address == null)
        {
            return NotFound();
        }

        return View(address);
    }

    // GET: Address/Create
    public IActionResult Create()
    {
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
        return View();
    }

    // POST: Address/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AddressID,Street,City,Postcode,State,CustomerID")] Address address)
    {
        if (ModelState.IsValid)
        {
            _context.Add(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", address.CustomerID);
        return View(address);
    }

    // GET: Address/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Address == null)
        {
            return NotFound();
        }

        var address = await _context.Address.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", address.CustomerID);
        return View(address);
    }

    // POST: Address/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AddressID,Street,City,Postcode,State,CustomerID")] Address address)
    {
        if (id != address.AddressID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(address);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(address.AddressID))
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
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", address.CustomerID);
        return View(address);
    }

    // GET: Address/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Address == null)
        {
            return NotFound();
        }

        var address = await _context.Address
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(m => m.AddressID == id);
        if (address == null)
        {
            return NotFound();
        }

        return View(address);
    }

    // POST: Address/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Address == null)
        {
            return Problem("Entity set 'MCBAContext.Address'  is null.");
        }
        var address = await _context.Address.FindAsync(id);
        if (address != null)
        {
            _context.Address.Remove(address);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AddressExists(int id)
    {
        return _context.Address.Any(e => e.AddressID == id);
    }
}

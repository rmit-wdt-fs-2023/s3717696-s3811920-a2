using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.API.Repositories;
public class BillPayRepository : IBillPayRepository
{
    private readonly MCBAContext _context;
    public BillPayRepository(MCBAContext context)
    {
        _context = context;
    }

    public async Task<BillPay> GetByIdAsync(int id)
    {
        return await _context.BillPay
            .Include(c => c.Account)
            .Include(z => z.Payee)
            .FirstOrDefaultAsync(m => m.BillPayID == id);
    }

    public async Task<List<BillPay>> GetAllAsync()
    {
        return await _context.BillPay
            .Include(c => c.Account)
            .Include(z => z.Payee)
            .ToListAsync();
    }

    public void Update(BillPay billPay)
    {
        _context.Update(billPay);
        _context.SaveChangesAsync();
    }
}


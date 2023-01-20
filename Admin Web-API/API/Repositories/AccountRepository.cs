using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.API.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly MCBAContext _context;

    public AccountRepository(MCBAContext context)
    {
        _context = context;
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        return await _context.Account
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(m => m.AccountNumber == id);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Account
            .Include(c => c.Transactions)
            .ToListAsync();
    }

    public async Task<List<Account>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.Account
            .Include(c => c.Transactions)
            .Where(x => x.CustomerID == customerId)
            .ToListAsync();
    }
}

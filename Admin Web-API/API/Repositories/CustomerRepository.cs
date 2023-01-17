using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MCBAContext _context;

    public CustomerRepository(MCBAContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customer
            .Include(c => c.Address)
            .Include(a => a.Accounts)
            .ThenInclude(t => t.Transactions)
            .Include(c => c.Login)
            .ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _context.Customer
            .Include(c => c.Address)
            .Include(a => a.Accounts)
            .ThenInclude(t => t.Transactions)
            .Include(c => c.Login)
            .FirstOrDefaultAsync(m => m.CustomerID == id);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        _context.Customer.Update(customer);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer == null)
            return false;

        _context.Customer.Remove(customer);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }
}

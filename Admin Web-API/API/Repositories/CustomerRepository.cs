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
            .ToListAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _context.Customer
            .Include(c => c.Address)
            .Include(a => a.Accounts)
            .ThenInclude(t => t.Transactions)
            .FirstOrDefaultAsync(m => m.CustomerID == id);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {

        _context.Customer.Update(customer);
        await _context.SaveChangesAsync();

    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customer.FindAsync(id);

        _context.Customer.Remove(customer);
        await _context.SaveChangesAsync();

    }
}

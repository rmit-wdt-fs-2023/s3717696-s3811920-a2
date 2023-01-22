using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services;

public class CustomerService : ICustomerService
{
    private readonly MCBAContext _context;

    public CustomerService(MCBAContext context)
    {
        _context = context;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customer.ToList();
    }

    public Customer GetById(int id)
    {
        return _context.Customer
            .Include(m => m.Address)
            .Include(x => x.Accounts)
            .ThenInclude(t => t.Transactions)
            .FirstOrDefault(t => t.CustomerID == id);
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
        _context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Remove(id);
        _context.SaveChanges();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}
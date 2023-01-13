using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MCBAContext _context;

    public CustomerRepository(MCBAContext context)
    {
        _context = context;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customers;
    }

    public Customer GetById(int id)
    {
        return _context.Customers.Find(id);
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var customer = _context.Customers.Find(id);
        _context.Customers.Remove(customer);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
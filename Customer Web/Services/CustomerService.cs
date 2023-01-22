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
        //return _context.GetAll();
        return _context.Customer.ToList();
    }

    public Customer GetById(int id)
    {
        //return _context.GetById(id);
        return null;
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
    }

    public void Delete(int id)
    {
        _context.Remove(id);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}
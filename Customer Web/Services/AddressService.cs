using MCBA_Web.Data;
using MCBA_Web.Models;

namespace MCBA_Web.Services;

public class AddressService : IAddressService
{
    private readonly MCBAContext _context;

    public AddressService(MCBAContext context)
    {
        _context = context;
    }

    public IEnumerable<Address> GetAll()
    {
        return _context.Address.ToList();
    }

    public Address GetById(int id)
    {
        return _context.Address.Find(id);
    }

    public void Add(int customerid, Address address)
    {
        address.CustomerID = customerid;

        _context.Address.Add(address);
        _context.SaveChanges();
    }

    public void Update(int customerId, Address address)
    {
        address.CustomerID = customerId;

        _context.Address.Update(address);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
		var address = _context.Address.Find(id);
        _context.Address.Remove(address);
        _context.SaveChanges();
    }
}


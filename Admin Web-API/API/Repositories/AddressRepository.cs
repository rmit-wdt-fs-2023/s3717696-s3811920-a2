using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.API.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly MCBAContext _context;

    public AddressRepository(MCBAContext context)
    {
        _context = context;
    }

    public async Task<List<Address>> GetAllAddressAsync()
    {
        return await _context.Address.ToListAsync();
    }

    public async Task<Address> GetAddressByIdAsync(int id)
    {
        return await _context.Address.FindAsync(id);
    }

    public async Task<Address> AddAddressAsync(Address address)
    {
        _context.Address.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    Task<bool> IAddressRepository.UpdateAddressAsync(Address address)
    {
        throw new NotImplementedException();
    }

    Task<bool> IAddressRepository.DeleteAddressAsync(int id)
    {
        throw new NotImplementedException();
    }

    //public async Task<bool> UpdateCustomerAsync(Customer customer)
    //{
    //    _context.Customer.Update(customer);
    //    var updated = await _context.SaveChangesAsync();
    //    return updated > 0;
    //}

    //public async Task<bool> DeleteCustomerAsync(int id)
    //{
    //    var customer = await _context.Customer.FindAsync(id);
    //    if (customer == null)
    //        return false;

    //    _context.Customer.Remove(customer);
    //    var deleted = await _context.SaveChangesAsync();
    //    return deleted > 0;
    //}
}

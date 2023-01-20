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

    public void UpdateAddress(Address address)
    {
        _context.Address.Update(address);
        _context.SaveChanges();

    }

    void IAddressRepository.DeleteAddressAsync(int id)
    {
        throw new NotImplementedException();
    }
}

using MCBA_Admin.Models;

namespace MCBA_Admin.API.Repositories;

public interface IAddressRepository
{
    Task<List<Address>> GetAllAddressAsync();
    Task<Address> GetAddressByIdAsync(int id);
    Task<Address> AddAddressAsync(Address customer);
    Task<bool> UpdateAddressAsync(Address customer);
    Task<bool> DeleteAddressAsync(int id);
}

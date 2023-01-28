using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address> GetByIdAsync(int id);
    void Add(Address customer);
    void Update(int id, Address customer);
    void Delete(int id);
}
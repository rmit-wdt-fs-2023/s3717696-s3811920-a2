using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    void Add(Customer customer);
    void Update(int id, Customer customer);
    void Delete(int id);
}
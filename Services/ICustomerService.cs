using MCBA_Web.Models;

namespace MCBA_Web.Services;

public interface ICustomerService
{
    IEnumerable<Customer> GetAll();
    Customer GetById(int id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
    void Save();
}
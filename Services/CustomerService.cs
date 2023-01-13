using MCBA_Web.Models;
using MCBA_Web.Repositories;

namespace MCBA_Web.Services;

public class CustomerService : ICustomerService
{
    private ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customerRepository.GetAll();
    }

    public Customer GetById(int id)
    {
        return _customerRepository.GetById(id);
    }

    public void Add(Customer customer)
    {
        _customerRepository.Add(customer);
    }

    public void Update(Customer customer)
    {
        _customerRepository.Update(customer);
    }

    public void Delete(int id)
    {
        _customerRepository.Delete(id);
    }

    public void Save()
    {
        _customerRepository.Save();
    }

}
using MCBA_Admin.API.Repositories;
using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _customerRepository.GetAllCustomersAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdAsync(id);
    }

    public void Add(Customer customer)
    {
        _customerRepository.AddCustomerAsync(customer);
    }

    public void Update(int id, Customer customer)
    {
        customer.CustomerID = id;
        _customerRepository.UpdateCustomerAsync(customer);
    }

    public void Delete(int id)
    {
        _customerRepository.DeleteCustomerAsync(id);
    }

    public void Save()
    {
        
    }

}
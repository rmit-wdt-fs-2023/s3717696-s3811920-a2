using MCBA_Admin.API.Repositories;
using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _addressRepository.GetAllAddressAsync();
    }

    public async Task<Address> GetByIdAsync(int id)
    {
        return await _addressRepository.GetAddressByIdAsync(id);
    }

    public void Add(Address address)
    {
        _addressRepository.AddAddressAsync(address);
    }

    public bool Update(int id, Address customerUpdated)
    {
        //var customer = 

        //if (customer == null)
        //{
        //    return false;
        //}

        //customer.Address = customerUpdated.Address;
        //customer.Accounts = customerUpdated.Accounts;
        //customerUpdated.CustomerID = id;
        _addressRepository.UpdateAddressAsync(customerUpdated);

        return true;
    }

    public void Delete(int id)
    {
        _addressRepository.DeleteAddressAsync(id);
    }

    public void Save()
    {
        
    }

}
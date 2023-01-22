using System;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web.Services;

public interface IAddressService
{
    IEnumerable<Address> GetAll();
    Address GetById(int id);
    void Add(int customerId, Address address);
    void Update(int customerId, Address address);
    void Delete(int id);
}


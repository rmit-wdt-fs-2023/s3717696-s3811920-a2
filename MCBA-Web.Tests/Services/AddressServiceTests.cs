using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Autofac;
using Xunit;
using MCBA_Web.Tests.Base;
using MCBA_Web.Services;
using MCBA_Web.Models;
using MCBA_Web.Data;
using Xunit.Sdk;

namespace MCBA_Web.Tests.Services;

public class AddressServiceTests : AbstractBackendTest
{
    private readonly MCBAContext _context;
    private readonly IAddressService _addressService;

    public AddressServiceTests()
    {
        _context = Container.Resolve<MCBAContext>();
        SeedData.InitializeForInMemoryDBContext(_context);

        _addressService = Container.Resolve<IAddressService>();
    }

    [Theory]
    [InlineData(1)]
    public void GetById_ShouldReturnAddress(int id)
    {
        // Act
        var result = _addressService.GetById(id);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAllAddresses_ShouldReturnAllAddressesInMemoryDatabase()
    {
        // Act
        var result = _addressService.GetAll();


        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Add_ShouldAddNewAddress()
    {
        // Arrange
        var address = new Address
        {
            Street = "TEST ADDRESS W",
            City = "Anytown",
            State = StateType.VIC,
            Postcode = "2144"
        };

        // Act
        _addressService.Add(1, address);

        // Assert
        var result = _addressService.GetAll().SingleOrDefault(x => x.Street == address.Street);
        Assert.Equal(address, result);
    }

    [Fact]
    public void Update_ShouldUpdateAddress()
    {
        // Arrange
        var address = new Address
        {
            Street = "TEST ADDRESS WW",
            City = "Anytown",
            State = StateType.VIC,
            Postcode = "2144"
        };

        // Act
        _addressService.Update(1, address);

        // Assert
        var result = _addressService.GetAll().SingleOrDefault(x => x.Street == address.Street);
        Assert.Equal(address, result);
    }

}



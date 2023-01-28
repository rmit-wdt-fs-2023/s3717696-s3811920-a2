using Autofac;
using Xunit;
using MCBA_Web.Tests.Base;
using MCBA_Web.Services;
using MCBA_Web.Models;
using MCBA_Web.Data;

namespace MCBA_Web.Tests.Services;

public class CustomerServiceTests : AbstractBackendTest
{
    private readonly MCBAContext _context;
    private readonly ICustomerService _customerService;

    
    public CustomerServiceTests()
    {
        _context = Container.Resolve<MCBAContext>();
        SeedData.InitializeForInMemoryDBContext(_context);
        _customerService = Container.Resolve<ICustomerService>();
    }

    [Fact]
    public void MakeDefaultProfilePicture_ShouldMakeProfilePictureDefault()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Test",
            TFN = "333 333 333",
            Mobile = "0421 421 213",
            HasDefaultProfilePicture = false,
            ProfilePicture = new byte[] { 0x20 },
            ProfilePictureContentType = "jpg"
        };

        // Act
        _customerService.MakeDefaultProfilePicture(customer);

        // Assert
        Assert.True(customer.HasDefaultProfilePicture);
    }

    [Fact]
    public void GetAll_ShouldReturnAllCustomers()
    {
        // Act
        var result = _customerService.GetAll();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Add_ShouldAddNewCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            CustomerID = 1001,
            Name = "Test",
            TFN = "333 333 333",
            Mobile = "0421 421 213"
        };

        // Act
        _customerService.Add(customer);
        _customerService.Save();
        // Assert
        var result = _customerService.GetById(customer.CustomerID);
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData(2100)]
    [InlineData(2200)]
    public void GetById_ShouldReturnCustomer(int id)
    {
        // Act
        var result = _customerService.GetById(id);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Update_ShouldUpdateCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "NEW TEST NAME",
            TFN = "333 333 333",
            Mobile = "0421 421 213",
            HasDefaultProfilePicture = true,
        };

        // Act
        _customerService.Update(customer);
        _customerService.Save();

        // Assert
        var result = _customerService.GetAll().SingleOrDefault(x => x.Name == customer.Name);
        Assert.Equal(customer, result);
    }
}



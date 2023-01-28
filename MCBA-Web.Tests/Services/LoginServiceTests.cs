using Autofac;
using Xunit;
using MCBA_Web.Tests.Base;
using MCBA_Web.Services;
using MCBA_Web.Data;

namespace MCBA_Web.Tests.Services;


public class LoginServiceTests : AbstractBackendTest
{
    private ILoginService _loginService;
    private MCBAContext _context;

    public LoginServiceTests()
    {
        _context = Container.Resolve<MCBAContext>();
        SeedData.InitializeForInMemoryDBContext(_context);
        _loginService = Container.Resolve<ILoginService>();
    }

    [Theory]
    [InlineData(2100)]
    public void AuthenticateCustomer_WithValidCredentials_ReturnsLogin(int customerId)
    {
        // Arrange
        string password = "abc123";
        int loginId = 12345678;

        // Act
        var result = _loginService.AuthenticateCustomer(loginId, password);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AuthenticateCustomer_WithInvalidCredentials_ReturnsNull()
    {
        // Arrange
        string password = "sdafsdaf";
        int loginId = 12345678;

        // Act
        var result = _loginService.AuthenticateCustomer(loginId, password);

        // Assert
        Assert.Null(result);

    }

    [Theory]
    [InlineData(2100)]
    public void IsLocked_WithUnLockedAccount_ReturnsFalse(int customerId)
    {
        // Arrange
        var login = _loginService.GetLoginByCustomerId(customerId);

        // Act
        var result = _loginService.IsLocked(login.LoginID);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(12345678)]
    public void UpdatePassword_UpdatesPassword(int loginId)
    {
        // Arrange
        string newPassword = "new_password";
        var login = _context.Login.Find(loginId);

        // Act
        _loginService.UpdatePassword(login, newPassword);

        // Assert
        var updatedLogin = _context.Login.Find(loginId);

        Assert.Equal(login.PasswordHash, updatedLogin.PasswordHash);
    }

    [Theory]
    [InlineData(2100)]
    public void GetLoginByCustomerId_ReturnsCorrectLogin(int customerId)
    {
        // Arrange & Act
        var result = _loginService.GetLoginByCustomerId(customerId);

        // Assert
        Assert.NotNull(result);
    }
}


using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Autofac;
using Xunit;
using MCBA_Web.Tests.Base;
using MCBA_Web.Services;
using MCBA_Web.Models;
using MCBA_Web.Data;



namespace MCBA_Web.Tests.Services
{
	public class AccountServiceTests : AbstractBackendTest
	{
        private readonly MCBAContext _context;

        private readonly IAccountService _accountService;


        public AccountServiceTests()
		{
            _context = Container.Resolve<MCBAContext>();
            SeedData.InitializeForInMemoryDBContext(_context);

            _accountService = Container.Resolve<IAccountService>();
        }


        [Theory]
        [InlineData(4100)]
        [InlineData(4200)]
        [InlineData(4300)]
        public async Task GetById_ShouldReturnAccount(int id)
        {
            var result = await _accountService.GetById(id);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(2100)]
        public void GetAllCustomerAccounts_ShouldReturnTwoAccounts(int customerID)
        {
            var result = _accountService.GetAllCustomerAccounts(customerID);
            Assert.Equal(2, result.Count());

        }


        [Fact]
        public void GetAllAccounts_ShouldReturnAllAccountsInMemoryDatabase()
        {
            var result = _accountService.GetAllAccounts();
            Assert.Equal(5, result.Count());
        }


        [Fact]
        public async Task CreateNewAccount_ShouldCreateNewAccount()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = 4400,
                AccountType = AccountType.Checking,
                Balance = 1000,
                CustomerID = 2200
            };

            // Act
            await _accountService.CreateNewAccount(account);

            // Assert
            var result = _accountService.GetAllAccounts().SingleOrDefault(x => x.AccountNumber == account.AccountNumber);
            Assert.Equal(account, result);
        }


        [Fact]
        public async Task UpdateAccountDetails_ShouldUpdateAccount()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = 4400,
                AccountType = AccountType.Checking,
                Balance = 5000,
                CustomerID = 2200
            };

            // Act
            await _accountService.UpdateAccountDetails(account);

            // Assert
            var result = _accountService.GetAllAccounts().SingleOrDefault(x => x.AccountNumber == account.AccountNumber);
            Assert.Equal(account, result);
        }


        [Fact]
        public async Task DeleteAccount_NonExistentEntity_ThrowsException()
        {
            await _accountService.DeleteAccount(4300);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _accountService.DeleteAccount(4300));
        }

    }
}


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
	public class PayeeServiceTests : AbstractBackendTest
    {
        private readonly MCBAContext _context;

        private readonly IPayeeService _payeeService;

        public PayeeServiceTests() 
		{
            _context = Container.Resolve<MCBAContext>();
            _payeeService = Container.Resolve<IPayeeService>();
            SeedData.InitializeForInMemoryDBContext(_context);
        }

        public override void Dispose() => _context.Dispose();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPayeeById_ShouldReturnPayee(int id)
        {
            var result = await _payeeService.GetPayeeById(id);
            Assert.NotNull(result);
        }


        [Fact]
        public void GetAllPayees_ShouldReturnAllPayees()
        {
            var result = _payeeService.GetAllPayees();
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async Task CreateNewPayee_ShouldCreateNewPayee()
        {
            // Arrange
            var payee = new Payee
            {
                PayeeID = 3,
                Name = "TPG",
                Phone = "0200200200",
                Street = "IJK Street",
                State = StateType.VIC,
                Postcode = "1111",
                City = "Melbourne"
            };

            // Act
            await _payeeService.CreateNewPayee(payee);

            // Assert
            var result = await _payeeService.GetPayeeById(payee.PayeeID);
            Assert.Equal(payee, result);
        }


        [Fact]
        public async Task UpdatePayeeDetails_ShouldCreateNewPayee()
        {
            // Arrange
            var payee = new Payee
            {
                PayeeID = 1,
                Name = "Telstra",
                Phone = "0300300300",
                Street = "WXY Street",
                State = StateType.NSW,
                Postcode = "1111",
                City = "Sydney"
            };

            // Act
            await _payeeService.UpdatePayeeDetails(payee);

            // Assert
            var result = await _payeeService.GetPayeeById(payee.PayeeID);
            Assert.Equal(payee, result);
        }


        [Fact]
        public void DeletePayee_NonExistentEntity_ThrowsException()
        {
            _payeeService.DeletePayee(2);
            var deletedPayee = _context.Payee.SingleOrDefault(e => e.PayeeID == 2);
            Assert.Null(deletedPayee);
        }

    }
}


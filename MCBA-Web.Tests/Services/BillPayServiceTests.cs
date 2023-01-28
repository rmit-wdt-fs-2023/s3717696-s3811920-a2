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
	public class BillPayServiceTests : AbstractBackendTest
    {
        private readonly MCBAContext _context;

        private readonly IBillPayService _billPayService;


        public BillPayServiceTests()
        {
            _context = Container.Resolve<MCBAContext>();
            SeedData.InitializeForInMemoryDBContext(_context);

            _billPayService = Container.Resolve<IBillPayService>();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetBillById_ShouldReturnBillPay(int id)
        {
            var result = await _billPayService.GetBillById(id);
            Assert.NotNull(result);
        }
    }
}


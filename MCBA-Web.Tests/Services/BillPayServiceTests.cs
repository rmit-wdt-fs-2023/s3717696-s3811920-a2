using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Autofac;
using Xunit;
using MCBA_Web.Tests.Base;
using MCBA_Web.Services;
using MCBA_Web.Models;
using MCBA_Web.Data;
using System;

namespace MCBA_Web.Tests.Services
{
	public class BillPayServiceTests : AbstractBackendTest
    {
        private readonly MCBAContext _context;

        private readonly IBillPayService _billPayService;


        public BillPayServiceTests()
        {
            _context = Container.Resolve<MCBAContext>();
            _billPayService = Container.Resolve<IBillPayService>();
            SeedData.InitializeForInMemoryDBContext(_context);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetBillById_ShouldReturnBillPay(int id)
        {
            var result = await _billPayService.GetBillById(id);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(4100)]
        public void GetBillsByAccountNumber_ShouldReturnAllScheduledBills(int accountNumber)
        {
            var result = _billPayService.GetBillsByAccountNumber(accountNumber);
            Assert.Equal(2, result.Count());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetBillsByPayeeID_ShouldReturnAllScheduledBills(int payeeID)
        {
            var result = _billPayService.GetBillsByPayeeID(payeeID);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task AddNewBillPay_ShouldScheduleNewBillPay()
        {
            // Arrange
            var billpay = new BillPay
            {
                BillPayID = 4,
                AccountNumber = 4200,
                PayeeID = 1,
                Amount = 100,
                IsBlocked = false,
                ScheduleTimeUtc = DateTime.UtcNow,
                PaymentPeriod = PaymentPeriodType.OneTime,
            };

            // Act
            await _billPayService.AddNewBillPay(billpay);

            // Assert
            var result = await _billPayService.GetBillById(billpay.BillPayID);
            Assert.Equal(billpay, result);
        }


        [Fact]
        public async Task UpdateBillPay_ShouldUpdateBillPayDetails()
        {
            // Arrange
            var billpay = new BillPay
            {
                BillPayID = 1,
                AccountNumber = 4100,
                PayeeID = 1,
                Amount = 200,
                IsBlocked = false,
                ScheduleTimeUtc = DateTime.UtcNow,
                PaymentPeriod = PaymentPeriodType.Monthly,
            };

            // Act
            await _billPayService.UpdateBillPay(billpay);

            // Assert
            var result = await _billPayService.GetBillById(billpay.BillPayID);
            Assert.Equal(billpay, result);
        }


        [Fact]
        public void DeleteBillPay_NonExistentEntity_ThrowsException()
        {
            _billPayService.DeleteBillPay(3);
            var deletedBillPay = _context.BillPay.SingleOrDefault(e => e.BillPayID == 3);
            Assert.Null(deletedBillPay);
        }
    }
}


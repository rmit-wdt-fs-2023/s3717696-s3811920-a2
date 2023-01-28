using Autofac;
using MCBA_Web.Data;
using MCBA_Web.Services;
using MCBA_Web.Tests.Base;
using Xunit;

namespace MCBA_Web.Tests.Services
{
	public class TransactionServiceTests : AbstractBackendTest
    {
        private readonly MCBAContext _context;

        private readonly ITransactionService _transactionService;

        public TransactionServiceTests()
		{
            _context = Container.Resolve<MCBAContext>();

            _transactionService = Container.Resolve<ITransactionService>();
            SeedData.InitializeForInMemoryDBContext(_context);
        }

        public override void Dispose() => _context.Dispose();


        [Theory]
        [InlineData(4200)]
        public async Task GetAccountTransactionsPerPage_ShouldReturnTransactions(int accountNumber)
        {
            var result = await _transactionService.GetAccountTransactionsPerPage(accountNumber);
            Assert.Equal(2, result.Count());
        }


        [Theory]
        [InlineData(4100)]
        public async Task GetBillPayTransactionsPerPage_ShouldReturnBillPayTransactions(int accountNumber)
        {
            var result = await _transactionService.GetBillPayTransactionsPerPage(accountNumber);
            Assert.Equal(2, result.Count());
        }

    }
}


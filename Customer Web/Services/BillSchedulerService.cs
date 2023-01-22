using System;
using System.Threading;
using MCBA_Web.Data;
using MCBA_Web.Models;
using MCBA_Web.Utilities;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services
{
    public class BillSchedulerService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BillSchedulerService> _logger;

        public BillSchedulerService(IServiceProvider services, ILogger<BillSchedulerService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BillPay Background Service: ExecuteAsync.");

            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteTask(cancellationToken);

                _logger.LogInformation("BillPay Background Service is waiting a minute.");

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async Task ExecuteTask(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BillPay Background Service is working.");

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MCBAContext>();

            var scheduledBills = await context.BillPay
                .FromSql($"SELECT * FROM BillPay WHERE ScheduleTimeUtc <= GETDATE()")
                .ToListAsync(cancellationToken);


            foreach (var bill in scheduledBills)
            {
                Account account = await context.Account.FindAsync(bill.AccountNumber);

                if (!account.HasInsufficientBalance(bill.Amount, account.AccountType))
                {
                    // update account balance
                    account.Balance -= bill.Amount;

                    // add billpay transaction
                    account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.BillPay,
                        Amount = bill.Amount,
                        TransactionTimeUtc = DateTime.UtcNow
                    });

                    // change status of the failed billpay or delete processed billpay
                    context.BillPay.Attach(bill);
                    context.BillPay.Remove(bill);
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("BillPay Background Service work complete.");
        }
    }
}


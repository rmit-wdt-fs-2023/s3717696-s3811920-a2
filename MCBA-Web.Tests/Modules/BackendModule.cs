using Autofac;
using MCBA_Web.Data;
using MCBA_Web.Controllers;
using MCBA_Web.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Tests.Modules
{
    public class BackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //base.Load(builder);

            // Substitute all ILogger types.
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            // Register types for DI (Dependency Injection).
            builder.Register(c => new MCBAContext(new DbContextOptionsBuilder<MCBAContext>().
            UseInMemoryDatabase(nameof(MCBAContext)).Options));

            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<BillPayService>().As<IBillPayService>();
            builder.RegisterType<PayeeService>().As<IPayeeService>();
        }
    }
}


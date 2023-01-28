using Autofac;
using MCBA_Web.Data;
using MCBA_Web.Controllers;
using MCBA_Web.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using Autofac.Core;

namespace MCBA_Web.Tests.Modules
{
    public class BackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<BillSchedulerService>().As<BillSchedulerService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<LoginService>().As<ILoginService>();

            builder.RegisterType<AccountController>().As<AccountController>();
            builder.RegisterType<AddressController>().As<AddressController>();
            builder.RegisterType<BillPayController>().As<BillPayController>();
            builder.RegisterType<CustomerController>().As<CustomerController>();
            builder.RegisterType<HomeController>().As<HomeController>();
            builder.RegisterType<LoginController>().As<LoginController>();
            builder.RegisterType<PayeeController>().As<PayeeController>();
            builder.RegisterType<TransactionController>().As<TransactionController>();
        }
    }
}


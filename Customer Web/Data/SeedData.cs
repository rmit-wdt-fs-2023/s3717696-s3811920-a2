using MCBA_Web.Data;
using MCBA_Web.Models;

namespace McbaExample.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<MCBAContext>();

        // Look for customers.
        if (context.Customer.Any())
        {
            Console.WriteLine("Skipping preload...");
            return; // DB has already been seeded.
        }

        // Run PreLoad
        PreLoad preLoad = new();
        preLoad.Run();

        // Commit New Data
        context.Customer.AddRangeAsync(preLoad.GetCustomers());
        context.Account.AddRangeAsync(preLoad.GetAccounts());
        context.Address.AddRangeAsync(preLoad.GetAddresses());
        context.Transaction.AddRangeAsync(preLoad.GetTransactions());
        context.Login.AddRangeAsync(preLoad.GetLogins());
        
        // Save
        context.SaveChanges();

    }
}

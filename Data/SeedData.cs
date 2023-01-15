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
            return; // DB has already been seeded.

    }
}

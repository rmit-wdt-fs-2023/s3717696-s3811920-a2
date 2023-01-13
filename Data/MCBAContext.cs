using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Data;

public class MCBAContext : DbContext
{

    public MCBAContext(DbContextOptions<MCBAContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }

}
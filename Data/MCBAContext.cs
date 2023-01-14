using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MCBA_Web.Data;

public class MCBAContext : DbContext
{

    public MCBAContext(DbContextOptions<MCBAContext> options) : base(options)
    {

    }

    public DbSet<Customer> Customer { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<BillPay> BillPay { get; set; }
    public DbSet<Payee> Payee { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Login> Login { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BillPay>()
        .HasOne(b => b.Payee)
        .WithMany()
        .OnDelete(DeleteBehavior.NoAction);
    }
}
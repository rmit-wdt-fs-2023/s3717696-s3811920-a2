using MCBA_Web.DTO;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace MCBA_Web.Data;

public class MCBAContext : DbContext
{

    public MCBAContext(DbContextOptions<MCBAContext> options) : base(options)
    {
    }

    // Models
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

        builder.Entity<Customer>().HasKey(c => c.CustomerID);
        builder.Entity<Customer>().Property(c => c.CustomerID).HasDefaultValueSql("1000");

        builder.Entity<Login>().HasKey(c => c.LoginID);
        builder.Entity<Login>().Property(c => c.LoginID).HasDefaultValueSql("10000000");

        builder.Entity<Account>().HasKey(c => c.AccountNumber);
        builder.Entity<Account>().Property(c => c.AccountNumber).HasDefaultValueSql("1000");

        builder.Entity<Account>()
            .Property(c => c.AccountType)
            .HasConversion(
                v => v.ToString(),
                v => (AccountType)Enum.Parse(typeof(AccountType), v));

        builder.Entity<Transaction>()
            .Property(c => c.TransactionType)
            .HasConversion(
                v => v.ToString(),
                v => (TransactionType)Enum.Parse(typeof(TransactionType), v));

        builder.Entity<Address>()
            .Property(c => c.State)
            .HasConversion(
                v => v.ToString(),
                v => (StateType)Enum.Parse(typeof(StateType), v));

        // Configure ambiguous Account.Transactions navigation property relationship.
        builder.Entity<Transaction>().
            HasOne(x => x.Account).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);
    }
}
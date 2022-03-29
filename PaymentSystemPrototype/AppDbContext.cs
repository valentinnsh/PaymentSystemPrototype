using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<UserRecord> Users { get; set; }
    public DbSet<BalanceRecord> Balances { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRecord>().ToTable("users").HasKey(x => x.Id);
        modelBuilder.Entity<UserRecord>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRecord>().Property(x => x.Email);
        modelBuilder.Entity<UserRecord>().Property(x => x.FirstName);
        modelBuilder.Entity<UserRecord>().Property(x => x.LastName);
        modelBuilder.Entity<UserRecord>().Property(x => x.Password);
        modelBuilder.Entity<UserRecord>().Property(x => x.RegisteredAt);

        modelBuilder.Entity<BalanceRecord>().Property(x => x.Amount);
        modelBuilder.Entity<BalanceRecord>().Property(x => x.UserId);

        modelBuilder.Entity<UserRecord>()
            .HasOne<BalanceRecord>(u => u.BalanceRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<BalanceRecord>(b => b.UserId);
    }
}
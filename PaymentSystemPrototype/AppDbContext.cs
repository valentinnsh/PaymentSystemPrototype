using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<UserRecord?> Users { get; set; }

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
    }
}
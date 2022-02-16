using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(k => k.Id);
        modelBuilder.Entity<User>().Property(x => x.Email);
        modelBuilder.Entity<User>().Property(x => x.FirstName);
        modelBuilder.Entity<User>().Property(x => x.LastName);
        modelBuilder.Entity<User>().Property(x => x.RegisteredAt);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
    }
}
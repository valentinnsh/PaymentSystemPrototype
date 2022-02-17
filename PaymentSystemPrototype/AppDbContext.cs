using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().ToTable("users");
        
        modelBuilder.Entity<User>().Property(k => k.Id).HasColumnName("id");
        modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnName("first_name");
        modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnName("last_name");
        modelBuilder.Entity<User>().Property(x => x.RegisteredAt).HasColumnName("registered_at");
    }
}
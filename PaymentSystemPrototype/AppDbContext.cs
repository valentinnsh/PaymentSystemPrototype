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
        modelBuilder.Entity<UserRecord>().Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRecord>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<UserRecord>().Property(x => x.FirstName).HasColumnName("first_name");
        modelBuilder.Entity<UserRecord>().Property(x => x.LastName).HasColumnName("last_name");
        modelBuilder.Entity<UserRecord>().Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<UserRecord>().Property(x => x.RegisteredAt).HasColumnName("registered_at");
    }
}
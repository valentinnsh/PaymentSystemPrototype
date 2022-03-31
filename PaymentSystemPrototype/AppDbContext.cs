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
    public DbSet<RoleRecord> Roles { get; set; }
    public DbSet<UserRoleRecord> UserRoles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserRecord>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRecord>().Property(x => x.Email);
        modelBuilder.Entity<UserRecord>().Property(x => x.FirstName);
        modelBuilder.Entity<UserRecord>().Property(x => x.LastName);
        modelBuilder.Entity<UserRecord>().Property(x => x.Password);
        modelBuilder.Entity<UserRecord>().Property(x => x.RegisteredAt);

        modelBuilder.Entity<BalanceRecord>().Property(x => x.Amount);
        modelBuilder.Entity<BalanceRecord>().Property(x => x.UserId);

        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.BalanceRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<BalanceRecord>(b => b.UserId);
        
        modelBuilder.Entity<RoleRecord>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<RoleRecord>().Property(x => x.Name);
        
        modelBuilder.Entity<UserRoleRecord>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRoleRecord>().Property(x => x.UserId);
        modelBuilder.Entity<UserRoleRecord>().Property(x => x.RoleId);
        
        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.UserRoleRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<UserRoleRecord>(b => b.UserId);

        // modelBuilder.Entity<UserRoleRecord>()
        //     .HasOne(x => x.RoleRecord)
        //     .WithMany(x => x.UserRoleRecord)
        //     .HasPrincipalKey(x =>x.UserRoleRecord);
        modelBuilder.Entity<RoleRecord>()
            .HasMany(x => x.UserRoleRecord)
            .WithOne(x => x.RoleRecord)
            .HasForeignKey(f => f.RoleId);
        // modelBuilder.Entity<RoleRecord>()
        //     .HasMany(u => u.UserRoleRecord)
        //     .WithOne(b => b.RoleId);

        modelBuilder.Entity<UserRecord>().HasData(
        new UserRecord { Id = 1, FirstName = "Igor", LastName = "Igorev", Email = "Igor@gmail.com",
            Password = "password", RegisteredAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)});

        modelBuilder.Entity<BalanceRecord>().HasData(
            new BalanceRecord{UserId = 1, Amount = 100});

        modelBuilder.Entity<RoleRecord>().HasData(
            new RoleRecord {Id = 1, Name = "User"},
            new RoleRecord {Id = 2, Name = "Admin"},
            new RoleRecord {Id = 3, Name = "KYC"},
            new RoleRecord {Id = 4, Name = "Funds Manager"});
        modelBuilder.Entity<UserRoleRecord>().HasData(
            new UserRoleRecord {Id = 1, UserId = 1, RoleId = 2}
        );
    }
}
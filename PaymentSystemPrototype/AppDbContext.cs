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
    public DbSet<VereficationRecord> Verefications { get; set; }
    public DbSet<TransferRecord> Transfers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserRecord>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserRecord>().Property(x => x.Email);
        modelBuilder.Entity<UserRecord>().Property(x => x.FirstName);
        modelBuilder.Entity<UserRecord>().Property(x => x.LastName);
        modelBuilder.Entity<UserRecord>().Property(x => x.Password);
        modelBuilder.Entity<UserRecord>().Property(x => x.RegisteredAt);
        modelBuilder.Entity<UserRecord>().Property(x => x.Block);

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
        
        modelBuilder.Entity<UserRoleRecord>()
            .HasOne(r => r.RoleRecord)
            .WithMany(ur => ur.UserRoleRecord)
            .HasForeignKey(fk=> fk.RoleId);

        modelBuilder.Entity<VereficationRecord>().Property(x => x.UserId);
        modelBuilder.Entity<VereficationRecord>().Property(x => x.Status);
        modelBuilder.Entity<VereficationRecord>().Property(x => x.LastChangeDate);
        modelBuilder.Entity<VereficationRecord>().Property(x => x.Reviewer);
        
        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.VereficationRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<VereficationRecord>(b => b.UserId);

        modelBuilder.Entity<TransferRecord>().Property(x => x.Id);
        modelBuilder.Entity<TransferRecord>().Property(x => x.Amount);
        modelBuilder.Entity<TransferRecord>().Property(x => x.CardNumber);
        modelBuilder.Entity<TransferRecord>().Property(x => x.ConfirmedAt);
        modelBuilder.Entity<TransferRecord>().Property(x => x.ConfirmedBy);
        modelBuilder.Entity<TransferRecord>().Property(x => x.CreatedAt);
        modelBuilder.Entity<TransferRecord>().Property(x => x.UserId);
        modelBuilder.Entity<TransferRecord>().Property(x => x.Status);
        modelBuilder.Entity<TransferRecord>()
            .HasOne(t => t.UserRecord)
            .WithMany(u => u.TransferRecords)
            .HasForeignKey(t=> t.UserId);
        
        modelBuilder.Entity<TransferRecord>()
            .HasOne(u => u.FundsUserRecord)
            .WithMany(t => t.ManagerTransferRecords)
            .HasForeignKey(t => t.ConfirmedBy)
            .IsRequired(false);

        modelBuilder.Entity<UserRecord>().HasData(
        new UserRecord { Id = 1, FirstName = "Igor", LastName = "Igorev", Email = "Igor@gmail.com",
            Password = "password", RegisteredAt = new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)});
        
        modelBuilder.Entity<BalanceRecord>().HasData(
            new BalanceRecord{UserId = 1, Amount = 100});

        modelBuilder.Entity<RoleRecord>().HasData(
            new RoleRecord {Id = 1, Name = "User"},
            new RoleRecord {Id = 2, Name = "Admin"},
            new RoleRecord {Id = 3, Name = "KYC"},
            new RoleRecord {Id = 4, Name = "Funds Manager"});
        modelBuilder.Entity<UserRoleRecord>().HasData(
            new UserRoleRecord {Id = 1, UserId = 1, RoleId = 3}
        );
        
    }
}
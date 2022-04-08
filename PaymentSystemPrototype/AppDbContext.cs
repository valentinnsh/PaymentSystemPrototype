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
        
        modelBuilder.Entity<UserRecord>().Property(it => it.Email).
            HasConversion(v => v.ToLowerInvariant(), v => v);
        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.BalanceRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<BalanceRecord>(b => b.UserId);

        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.UserRoleRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<UserRoleRecord>(b => b.UserId);
        
        modelBuilder.Entity<UserRoleRecord>()
            .HasOne(r => r.RoleRecord)
            .WithMany(ur => ur.UserRoleRecord)
            .HasForeignKey(fk=> fk.RoleId);

        modelBuilder.Entity<UserRecord>()
            .HasOne(u => u.VereficationRecord)
            .WithOne(b => b.UserRecord)
            .HasForeignKey<VereficationRecord>(b => b.UserId);
        
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
        new UserRecord { Id = 1, FirstName = "Admin", LastName = "Admin", Email = "admin@gmail.com",
            Password = "admin", RegisteredAt = new DateTime(2022, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)},
        new UserRecord { Id = 2, FirstName = "Kyc", LastName = "Kyc", Email = "kyc@gmail.com",
            Password = "kyc", RegisteredAt = new DateTime(2021, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)},
        new UserRecord { Id = 3, FirstName = "Funds", LastName = "Funds", Email = "funds@gmail.com",
            Password = "funds", RegisteredAt = new DateTime(2020, 3, 31, 17, 29, 3, 605, DateTimeKind.Utc)},
        new UserRecord { Id = 4, FirstName = "User1", LastName = "User1", Email = "user1@gmail.com",
            Password = "user1", RegisteredAt = new DateTime(2022, 3, 30, 17, 29, 3, 605, DateTimeKind.Utc)},
        new UserRecord { Id = 5, FirstName = "User2", LastName = "User2", Email = "user2@gmail.com",
            Password = "user2", RegisteredAt = new DateTime(2022, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc)}
        );
        
        modelBuilder.Entity<BalanceRecord>().HasData(
            new BalanceRecord{UserId = 1, Amount = 100},
            new BalanceRecord{UserId = 2, Amount = 100},
            new BalanceRecord{UserId = 3, Amount = 100},
            new BalanceRecord{UserId = 4, Amount = 100},
            new BalanceRecord{UserId = 5, Amount = 100});

        modelBuilder.Entity<RoleRecord>().HasData(
            new RoleRecord {Id = 1, Name = "User"},
            new RoleRecord {Id = 2, Name = "Admin"},
            new RoleRecord {Id = 3, Name = "KycManager"},
            new RoleRecord {Id = 4, Name = "FundsManager"});
        modelBuilder.Entity<UserRoleRecord>().HasData(
            new UserRoleRecord {Id = 1, UserId = 1, RoleId = 2},
            new UserRoleRecord {Id = 2, UserId = 2, RoleId = 3},
            new UserRoleRecord {Id = 3, UserId = 3, RoleId = 4},
            new UserRoleRecord {Id = 4, UserId = 4, RoleId = 1},
            new UserRoleRecord {Id = 5, UserId = 5, RoleId = 1}
        );

        modelBuilder.Entity<VereficationRecord>().HasData(
            new VereficationRecord {LastChangeDate = new DateTime(2022, 3, 29, 14,
                29, 3, 605, DateTimeKind.Utc), Reviewer = null, Status = 2, UserId = 4},
            new VereficationRecord {LastChangeDate = new DateTime(2022, 3, 28, 14,
                29, 3, 605, DateTimeKind.Utc), Reviewer = null, Status = 2, UserId = 5}
            );

        modelBuilder.Entity<TransferRecord>().HasData(
            new TransferRecord {
                Id = 1, Amount = 100, CardNumber = 1234567812345678,ConfirmedAt = null,ConfirmedBy = null,
                CreatedAt =  new DateTime(2022, 3, 28, 14,
                    29, 3, 605, DateTimeKind.Utc),Status = 2,UserId = 4},
            new TransferRecord {
                Id = 2, Amount = -10, CardNumber = 8765432112345678,ConfirmedAt = null,ConfirmedBy = null,
                CreatedAt =  new DateTime(2022, 3, 24, 11,
                    29, 3, 605, DateTimeKind.Utc),Status = 2,UserId = 5}
            );

    }
}
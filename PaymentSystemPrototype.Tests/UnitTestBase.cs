using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests;


public class UnitTestBase
{
    protected UserOperationsService userOperationsService { get; set; }
    protected TransferOperationsService transferOperationsService { get; set; }
    private AppDbContext DbContext { get; set; }
    [SetUp]
    public void Prepare()
    {
        DbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("database_name").Options);
        DbContext.Database.EnsureCreated();
        
        userOperationsService = new UserOperationsService(DbContext);
        transferOperationsService = new TransferOperationsService(DbContext, userOperationsService);
        
        DbContext.Users.Add(new UserRecord
        {
            FirstName = "Igor", LastName = "Igorev", Email = "igor@gmail.com",
            Password = "igor", 
            RegisteredAt = new DateTime(2020, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc)
            
        });

        DbContext.Users.Add(new UserRecord
        {
            FirstName = "Elen", LastName = "Elenova", Email = "elen@gmail.com",
            Password = "elen", 
            RegisteredAt = new DateTime(2020, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc)
        });

        DbContext.SaveChanges();
        
        DbContext.Balances.Add(new BalanceRecord{UserId = 6, Amount = 100});
        DbContext.Balances.Add(new BalanceRecord{UserId = 7, Amount = 100});

        DbContext.UserRoles.Add(new UserRoleRecord 
            {UserId = 6, RoleId = 1});
        DbContext.UserRoles.Add(new UserRoleRecord
            {UserId = 7, RoleId = 2});

        DbContext.Transfers.Add(new TransferRecord {
            Id = 3, Amount = 100, CardNumber = 1234567812345678,ConfirmedAt = null,ConfirmedBy = null,
            CreatedAt =  new DateTime(2022, 3, 28, 14,
                29, 3, 605, DateTimeKind.Utc),Status = 2,UserId = 6});
        
        DbContext.Transfers.Add(new TransferRecord {
            Id = 4, Amount = -200, CardNumber = 1234567812345678,ConfirmedAt = null,ConfirmedBy = null,
            CreatedAt =  new DateTime(2022, 3, 28, 14,
                29, 3, 605, DateTimeKind.Utc),Status = 2,UserId = 6});
        
        DbContext.SaveChanges();
    }
    
    [TearDown]
    public void TeatDown()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}
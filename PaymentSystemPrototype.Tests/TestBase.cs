using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests;

[TestFixture]
public class TestBase
{
    protected TestEnvironment Env { get; set; }
    protected HttpClient Client { get; set; }
    protected HttpClient AuthUserClient { get; set; } 

    protected static UserOperationsService userOperationsService { get; set; }
    protected AppDbContext DbContext { get; set; }
    [OneTimeSetUp]
    public void Init()
    {
        Env = new TestEnvironment();
        Env.Start();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Env.Dispose();
        Client.Dispose();
    }

    [SetUp]
    public void Prepare()
    {
        Env.Prepare();
        Client = Env.WebAppHost.GetClient();
        DbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("database_name").Options);
        DbContext.Database.EnsureCreated();
        userOperationsService = new UserOperationsService(DbContext);

        DbContext.Users.Add(new UserRecord
        {
            FirstName = "Igor", LastName = "Igorev", Email = "igor@gmail.com",
            Password = "igor", RegisteredAt = new DateTime(2020, 3, 29, 17, 29, 3, 605, DateTimeKind.Utc)
        });
        
        DbContext.UserRoles.Add(new UserRoleRecord 
            {UserId = 6, RoleId = 1});
        DbContext.SaveChanges();
    }

    [TearDown]
    public void TeatDown()
    {
        DbContext.Dispose();
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests;

public class AuthTests : TestBase
{
    public IUserOperationsService userOperationsService;
    public IAuthService authService;
    
    [SetUp]
    public void PrepareForAuthTests()
    {
        userOperationsService = new UserOperationsService(DbContext);
        authService = new AuthService(userOperationsService);
    }
    
    [TestCase("admin@gmail.com", "admin", false)]
    public void LogInTest(string email, string password,bool expectedResult)
    {
        var result= authService.LogInAsync(email, password, new DefaultHttpContext());
        result.Result.Should().Be(expectedResult);
    }
}
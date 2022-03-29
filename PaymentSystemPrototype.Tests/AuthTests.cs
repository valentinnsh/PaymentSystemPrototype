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

namespace PaymentSystemPrototype.Tests;

public class AuthTests : TestBase
{
    [TestCase("Igor@gmail.com","password", HttpStatusCode.OK)]
    [TestCase("Igor@gmail.com","WrongPassword", HttpStatusCode.Forbidden)]
    [TestCase("NotIgor@gmail.com", "notapassword", HttpStatusCode.NotFound)]
    public void LogInTest(string email, string password,HttpStatusCode expectedResult)
    {
        var data = new LogInData {Email = email, Password = password};
        var result =  Client.PostAsJsonAsync("log_in", data);
        result.Result.StatusCode.Should().Be(expectedResult);
    }

    [Test]
    public void LogOutTest()
    {
        var result = AuthUserClient.PostAsync("auth/log_out", null).Result;
        result.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }
    
    [TestCase("Ivan","Ivanov","ivan@gmail.com", "qwerty", HttpStatusCode.OK)]
    [TestCase("Ivan","Ivanov","ivan@gmail.com", "qwerty", HttpStatusCode.Conflict)]
    public void SignUpTest(string firstName, string lastName, string email, string password,
        HttpStatusCode expectedResult)
    {
        var signUpData = new SignUpData
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        var result = Client.PostAsJsonAsync("/sign_up", signUpData);
        result.Result.StatusCode.Should().Be(expectedResult);
    }
}
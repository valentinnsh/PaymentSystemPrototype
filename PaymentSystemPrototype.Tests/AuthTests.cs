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
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace PaymentSystemPrototype.Tests;

public class AuthTests : TestBase
{
    [TestCase("Igor@gmail.com","password", HttpStatusCode.OK)]
    [TestCase("Igor@gmail.com","WrongPassword", HttpStatusCode.Forbidden)]
    [TestCase("NotIgor@gmail.com", "notapassword", HttpStatusCode.NotFound)]
    public void LogInTest(string email, string password,HttpStatusCode expectedResult)
    {
        var payload = new Dictionary<string, string>
        {
            {"Email", $"{email}"},
            {"Password", $"{password}"}
        };
        
        string strPayload = JsonConvert.SerializeObject(payload);
        var cont = new StringContent(strPayload, Encoding.UTF8, "application/json");
        var result = Client.PostAsync("auth/log_in", cont).Result;
        result.StatusCode.Should().Be(expectedResult);
    }

    [TestCase(HttpStatusCode.Accepted)]
    public void LogOutTest(HttpStatusCode expextedResult)
    {
        var result = AuthUserClient.PostAsync("auth/log_out", null).Result;
        result.StatusCode.Should().Be(expextedResult);
    }
    [TestCase("Ivan","Ivanov","ivan@gmail.com", "qwerty", HttpStatusCode.OK)]
    [TestCase("Ivan","Ivanov","ivan@gmail.com", "qwerty", HttpStatusCode.Conflict)]
    public void SignUpTest(string firstName, string lastName, string email, string password,
        HttpStatusCode expectedResult)
    {
        var payload = new Dictionary<string, string>
        {
            {"FirstName", $"{firstName}"},
            {"LastName", $"{lastName}"},
            {"Email", $"{email}"},
            {"Password", $"{password}"}
        };
        string strPayload = JsonConvert.SerializeObject(payload);
        var cont = new StringContent(strPayload, Encoding.UTF8, "application/json");
        var result = Client.PostAsync("auth/sign_up", cont).Result;
        result.StatusCode.Should().Be(expectedResult);
    }
}
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
        //string payload = $"{email}";
        string strPayload = JsonConvert.SerializeObject(payload);
        var cont = new StringContent(strPayload, Encoding.UTF8, "application/json");
        var result = Client.PostAsync("auth/log_in", cont).Result;
        result.StatusCode.Should().Be(expectedResult);
    }
}
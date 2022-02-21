using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace PaymentSystemPrototype.Tests;

public class AuthTests : TestBase
{
    [Test]
    public void DBconnectionTest() =>
        Client.GetAsync("auth/get-gmails").Result.StatusCode.Should().Be(HttpStatusCode.OK);

    
    [TestCase("Igor@gmail.com",HttpStatusCode.OK)]
    [TestCase("NotIgor@gmail.com", HttpStatusCode.NotFound)]
    public void LogInTest(string email, HttpStatusCode expectedResult)
    {
        var payload = new Dictionary<string, string>
        {
            {"Email", $"{email}"}
        };

        string strPayload = JsonConvert.SerializeObject(payload);
        HttpContent cont = new StringContent(strPayload, Encoding.UTF8, "application/json");
        var result = Client.PostAsync("auth/log_in", cont).Result;
        result.StatusCode.Should().Be(expectedResult);
    }
}
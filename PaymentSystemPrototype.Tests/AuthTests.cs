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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests;

[TestFixture]
public class AuthTests : TestBase
{
    [TestCase("admin@gmail.com", "admin")]
    public async Task LogInIntegrationTest(string email, string password)
    {
        var logInData = new FormUrlEncodedContent(new Dictionary<string, string> {{"Email", email}, {"Password", password}});
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Auth/LogIn/") {Content = logInData};

        var response = await Client.SendAsync(requestMessage);
        
        // Does not work. Local path is "/Auth/LogIn/" instead 
        response.RequestMessage?.RequestUri?.LocalPath.Should().Be("/Auth/UserProfile/"); 
    }
}
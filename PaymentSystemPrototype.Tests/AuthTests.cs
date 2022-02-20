using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;

namespace PaymentSystemPrototype.Tests;

public class AuthTests : TestBase
{
    [Test]
    public void DBconnectionTest() =>
        Client.GetAsync("auth/get-gmails").Result.StatusCode.Should().Be(HttpStatusCode.OK);
}
using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PaymentSystemPrototype;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc.Testing;


namespace PaymentSystemPrototype.Tests;

public class TestHost : IDisposable
{
    public TestServer _testServer;
    public IServiceProvider Services => _testServer.Host.Services;

    public HttpClient Client;

    public void Start()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(_ => {});
            });

        _testServer = application.Server;
        Client = _testServer.CreateClient();
    }

    public HttpClient GetClient() => Client;

    public void Dispose()
    {
        _testServer?.Dispose();
    }
}
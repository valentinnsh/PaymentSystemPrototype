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
    private TestServer _testServer;
    public IServiceProvider Services => _testServer.Host.Services;

    public void Start()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                });
            });

        _testServer = application.Server;
    }

    public HttpClient GetClient() => _testServer.CreateClient();

    public void Dispose()
    {
        _testServer?.Dispose();
    }
}
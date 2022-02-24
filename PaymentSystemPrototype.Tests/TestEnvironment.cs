using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace PaymentSystemPrototype.Tests;

public class TestEnvironment : IDisposable
{
    public TestHost WebAppHost { get; }

    public TestEnvironment()
    {
        WebAppHost = new TestHost();
    }

    public void Start()
    {
        WebAppHost.Start();
    }

    public void Prepare()
    {
    }

    public void Dispose()
    {
        WebAppHost?.Dispose();
    }
}
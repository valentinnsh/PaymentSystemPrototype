using System.Collections.Generic;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Tests;

[TestFixture]
public class TestBase
{
    protected TestEnvironment Env { get; set; }
    protected HttpClient Client { get; set; }
    protected HttpClient AuthUserClient { get; set; } 

    [OneTimeSetUp]
    public void Init()
    {
        Env = new TestEnvironment();
        Env.Start();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Env.Dispose();
        Client.Dispose();
    }

    [SetUp]
    public void Prepare()
    {
        Env.Prepare();
        Client = Env.WebAppHost.GetClient();
    }
    
}
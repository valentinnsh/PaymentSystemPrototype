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
        AuthUserClient = CreateAuthorizedClientAsync().Result;
    }
    
    protected async Task<HttpClient> CreateAuthorizedClientAsync()
    {
        var client = Env.WebAppHost.GetClient();
        var payload = new Dictionary<string, string>
        {
            {"Email", "Igor@gmail.com"},
            {"Password", "password"}
        };
        
        string strPayload = JsonConvert.SerializeObject(payload);
        //var cont = new StringContent(strPayload, Encoding.UTF8, "application/json");
        var cont = new LogInData{Email = "Igor@gmail.com", Password = "password"};
        var res = await client.PostAsJsonAsync("auth/log_in", cont);
        client.DefaultRequestHeaders.Add(HeaderNames.Cookie, res.Headers.GetValues(HeaderNames.SetCookie));
        return client;
    }
}
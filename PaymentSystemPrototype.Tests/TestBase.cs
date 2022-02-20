using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

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
        //AliceClient = CreateAuthorizedClientAsync("alice@mailinator.com").GetAwaiter().GetResult();
        //BobClient = CreateAuthorizedClientAsync("bob@mailinator.com").GetAwaiter().GetResult();
    }

    // protected async Task<HttpClient> CreateAuthorizedClientAsync(string login)
    // {
    //     var client = Env.WebAppHost.GetClient();
    //     var res = await client.SignInAsync(login);
    //     client.DefaultRequestHeaders.Add(HeaderNames.Cookie, res.Headers.GetValues(HeaderNames.SetCookie));
    //     return client;
    // }
}
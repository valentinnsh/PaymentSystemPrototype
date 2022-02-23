using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Controllers;

[Route("auth")]
public class AuthControllers : Controller
{
    private readonly AppDbContext _db;
    private readonly IAuthService _authService;

    public AuthControllers(AppDbContext context, IAuthService serv)
    {
        _db = context;
        _authService = serv;
    }

    [HttpPost("sign_up")]
    public Task<ContentResult> SignUp([FromBody] Dictionary<string,string> newUser)
    {
        var result = _authService.SignUpAsync(newUser, HttpContext);
        if (result.Result == HttpStatusCode.Accepted)
        {
            return Task.FromResult(new ContentResult {StatusCode = StatusCodes.Status200OK});
        }
        return Task.FromResult(new ContentResult {StatusCode = StatusCodes.Status500InternalServerError});
    }

    [HttpPost("log_in")]
    public async Task<ContentResult> LogIn([FromBody] Dictionary<string, string> loginContent)
    {
        var userEmail = loginContent["Email"];
        var userPassword = loginContent["Password"];
        var result = await _authService.LoginAsync(userEmail, userPassword, HttpContext);
        return result switch
        {
            HttpStatusCode.OK => new ContentResult {StatusCode = StatusCodes.Status200OK},
            HttpStatusCode.Forbidden => new ContentResult {StatusCode = StatusCodes.Status403Forbidden},
            HttpStatusCode.NotFound => new ContentResult {StatusCode = StatusCodes.Status404NotFound},
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpPost("log_out")]
    public async Task<ContentResult> LogOut()
    {
        await _authService.LogOutAsync(HttpContext);
        return new ContentResult
        {
            StatusCode = StatusCodes.Status202Accepted, 
            Content = "Successfully signed out."
        };
    }
}

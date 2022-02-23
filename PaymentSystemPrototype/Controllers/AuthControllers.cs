using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

    [HttpPost("log_in")]
    public async Task<ContentResult> Login([FromBody] Dictionary<string, string> loginContent)
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

}

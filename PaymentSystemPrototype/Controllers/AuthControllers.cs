using System.Net;
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
        //_authService = new AuthService(new UserOperationsService(context));
        _authService = serv;
    }

    [HttpPost("log_in")]
    public async Task<ContentResult> Login([FromBody] string userEmail)
    {
        var result = await _authService.LoginAsync(userEmail, HttpContext);
        return result switch
        {
            HttpStatusCode.OK => new ContentResult {StatusCode = StatusCodes.Status202Accepted},
            HttpStatusCode.NotFound => new ContentResult {StatusCode = StatusCodes.Status404NotFound}
        };
    }
    
    [HttpGet("get-gmails")]
    public object Get()
    {
        return _db.Users.Where(b => b.Email.Contains("gmail")).Select((c) => new
        {
            c.Id,
            c.FirstName,
            c.LastName
        }).ToList();
    }
}
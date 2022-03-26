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
public class AuthController : Controller
{
    private readonly AppDbContext _db;
    private readonly IAuthService _authService;

    public AuthController(AppDbContext context, IAuthService serv)
    {
        _db = context;
        _authService = serv;
    }

    [HttpGet("sign_up")]
    public IActionResult SignUpPage()
    {
        return View("SignUp");
    }
    [HttpPost("sign_up")]
    public async Task<ActionResult> SignUp([FromBody] SignUpData signUpData)
    {
        var result = _authService.SignUpAsync(signUpData);
        if (result.Result is HttpStatusCode.OK or HttpStatusCode.Conflict)
        {
            Response.StatusCode = (int) result.Result;
            return View("SignUp");
            //return Task.FromResult(new ContentResult {StatusCode = (int) result.Result});
        }
        //return Task.FromResult(new ContentResult {StatusCode = StatusCodes.Status400BadRequest});
        Response.StatusCode = StatusCodes.Status400BadRequest;
        return View("SignUp");
    }

    [HttpGet("log_in")]
    public IActionResult LogInPage()
    {
        return View("LogIn");
    }
    [HttpPost("log_in")]
    public async Task<ActionResult> LogIn([FromBody] LogInData loginContent)
    {
        Response.StatusCode = (int)await _authService.LogInAsync(loginContent.Email, loginContent.Password, HttpContext);
        return View("LogIn");
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

    // [HttpPost("del")]
    // public async Task<ContentResult> DeleteUser([FromBody])
    // {
    //     
    // }
}

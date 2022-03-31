using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class AuthService : IAuthService
{
    private readonly IUserOperationsService _userOperationsService;

    public AuthService(IUserOperationsService userOperationsService)
    {
        _userOperationsService = userOperationsService;
    }
    
    public async Task<HttpStatusCode> LogInAsync(string userEmail, string userPassword, HttpContext httpContext)
    {
        if (_userOperationsService.FindByEmail(userEmail) == null)
        {
            return HttpStatusCode.NotFound;
        }
        var account = await _userOperationsService.CheckLoginInfo(userEmail, userPassword);
        if (account != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, _userOperationsService.GetUserRoleAsString(account.Email))
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
                
            return HttpStatusCode.OK;
        }
        return HttpStatusCode.Forbidden;
    }

    public Task LogOutAsync(HttpContext httpContext)
    {
        return httpContext.SignOutAsync();
    }

    public async Task<HttpStatusCode> SignUpAsync(SignUpData signUpData)
    {
        // Check if email is already in use
        if (_userOperationsService.FindByEmail(signUpData.Email) != null)
        {
            return HttpStatusCode.Conflict;
        }

        var newUser = new UserRecord
        {
            FirstName = signUpData.FirstName,
            LastName = signUpData.LastName,
            Email = signUpData.Email,
            Password = signUpData.Password,
            RegisteredAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)
        };
        await _userOperationsService.AddUserAsync(newUser);
        return HttpStatusCode.OK;
    }
}
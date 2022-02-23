using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class AuthService : IAuthService
{
    private readonly IUserOperationsService _uos;

    public AuthService(IUserOperationsService uos)
    {
        _uos = uos;
    }
    
    public async Task<HttpStatusCode> LoginAsync(string userEmail, string userPassword, HttpContext httpContext)
    {
        // TODO Move this null-check to CheckLoginInfo
        if (await _uos.FindByEmail(userEmail) == null)
        {
            return HttpStatusCode.NotFound;
        }
        var account = await _uos.CheckLoginInfo(userEmail, userPassword);
        if (account != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.FirstName),
                new Claim(ClaimTypes.Email, account.Email),
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

    public async Task<HttpStatusCode> SignUpAsync(Dictionary<string, string> userData, HttpContext httpContext)
    {
        var newUser = new UserRecord
        {
            FirstName = userData["FirstName"],
            LastName = userData["LastName"],
            Email = userData["Email"],
            Password = userData["Password"],
            RegisteredAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)
        };
        await _uos.AddUserAsync(newUser);
        return await Task.FromResult(HttpStatusCode.Accepted);
    }
}
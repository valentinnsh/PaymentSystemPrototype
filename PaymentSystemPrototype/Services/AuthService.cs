using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace PaymentSystemPrototype.Services;

public class AuthService : IAuthService
{
    private readonly IUserOperationsService _uos;

    public AuthService(IUserOperationsService uos)
    {
        _uos = uos;
    }
    
    public async Task<HttpStatusCode> LoginAsync(string userEmail, HttpContext httpContext)
    {
        var account = await _uos.FindByEmail(userEmail);
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
        return HttpStatusCode.NotFound;
    }
}
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
    
    public async Task<bool> LogInAsync(string userEmail, string userPassword, HttpContext httpContext)
    {
        if (await _userOperationsService.FindByEmailAsync(userEmail) == null)
        {
            throw new Exception();
        }
        var account = await _userOperationsService.CheckLoginInfoAsync(userEmail, userPassword);
        if (account != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, account.Email),
                new(ClaimTypes.Role, _userOperationsService.GetUserRoleAsString(account.Email)),
                new(ClaimTypes.NameIdentifier, account.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));
                
            return true;
        }
        return false;
    }

    public async Task LogOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync();
    }

    public async Task<bool> SignUpAsync(SignUpData signUpData)
    {
        // Check if email is already in use
        if (await _userOperationsService.FindByEmailAsync(signUpData.Email) != null)
        {
            return false;
        }

        var newUser = new UserRecord
        {
            FirstName = signUpData.FirstName,
            LastName = signUpData.LastName,
            Email = signUpData.Email,
            Password = signUpData.Password,
            RegisteredAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.UtcNow)
        };
        await _userOperationsService.AddUserAsync(newUser);
        return true;
    }
    
}
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using PaymentSystemPrototype.Exceptions;
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
            throw new UserNotFoundException();
        }
        var account = await _userOperationsService.CheckLoginInfoAsync(userEmail, userPassword);
        if (account == null) return false;
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, account.Email),
            new(ClaimTypes.Role,  _userOperationsService.GetUserRoleAsString(account.Id)),
            new(ClaimTypes.NameIdentifier, account.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));
                
        return true;
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
            RegisteredAt = DateTime.UtcNow
        };
        await _userOperationsService.AddUserAsync(newUser);
        return true;
    }
    
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IAuthService
{
    Task<bool> LogInAsync(string userEmail, string userPassword, HttpContext httpContext);
    Task LogOutAsync(HttpContext httpContext);
    Task<bool> SignUpAsync(SignUpData signUpData);
}
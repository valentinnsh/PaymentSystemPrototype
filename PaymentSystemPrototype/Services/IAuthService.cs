using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSystemPrototype.Services;

public interface IAuthService
{
    Task<HttpStatusCode> LogInAsync(string userEmail, string userPassword, HttpContext httpContext);
    Task LogOutAsync(HttpContext httpContext);
    Task<HttpStatusCode> SignUpAsync(Dictionary<string, string> userData);
}
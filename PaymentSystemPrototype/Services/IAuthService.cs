using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSystemPrototype.Services;

public interface IAuthService
{
    Task<HttpStatusCode> LoginAsync(string userEmail, string userPassword, HttpContext httpContext);
    Task LogOutAsync(HttpContext httpContext);
    Task<HttpStatusCode> SignUpAsync(Dictionary<string, string> userData, HttpContext httpContext);
}
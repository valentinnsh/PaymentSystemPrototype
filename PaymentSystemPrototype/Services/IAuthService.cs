using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSystemPrototype.Services;

public interface IAuthService
{
    Task<HttpStatusCode> LoginAsync(string userEmail, HttpContext httpContext);
}
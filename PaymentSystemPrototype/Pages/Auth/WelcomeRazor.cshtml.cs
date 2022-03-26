using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Pages.Auth;
[IgnoreAntiforgeryToken]
public class WelcomeRazor : PageModel
{
    public string Message { get; private set; } = "PageModel in C#";

    public void OnGet()
    {
        Message += $" Server time is { DateTime.Now }";
    }
    
    public async Task<ContentResult> OnPost([FromBody] LogInData logInData)
    {
        //Message = loginData.Email + "\n" + loginData.Password;
        if (logInData.Email == "good")
        {
            Response.StatusCode = StatusCodes.Status200OK;
        }
        else
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        return new ContentResult();
    }
}
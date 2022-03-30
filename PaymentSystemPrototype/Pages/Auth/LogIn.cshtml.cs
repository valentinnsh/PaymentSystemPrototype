using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Auth;

[IgnoreAntiforgeryToken]
public class LogIn : PageModel
{
    public string Message = "";
    public async Task<PageResult> OnGet(string msg)
    {
        Message += msg;
        return Page();
    }
    
    public async Task<ActionResult> OnPost([FromServices] IAuthService authService, [FromForm] LogInData loginContent)
    {
        var loginResult = await authService.LogInAsync(loginContent.Email, loginContent.Password, HttpContext);
        
        return loginResult switch
        {
            HttpStatusCode.OK => RedirectToPage("../Profiles/UserProfile", loginContent),
            HttpStatusCode.NotFound => RedirectToPage("LogIn",
                new {msg = "Email not found. Consider Signing Up first."}),
            HttpStatusCode.Forbidden => RedirectToPage("LogIn",
                new {msg = "Wrong Password"}),
            _ => RedirectToPage("LogIn",
                new {msg = "Unknown Error, please try again."})
        };
    }
}
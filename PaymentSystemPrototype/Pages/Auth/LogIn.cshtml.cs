using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Auth;

[IgnoreAntiforgeryToken]
public class LogIn : PageModel
{
    public async Task<PageResult> OnGet()
    {
        return Page();
    }
    
    public async Task<ActionResult> OnPost([FromServices] IAuthService authService, [FromForm] LogInData loginContent)
    {
        Response.StatusCode = (int)await authService.LogInAsync(loginContent.Email, loginContent.Password, HttpContext);
        if (Response.StatusCode != StatusCodes.Status200OK)
            return StatusCode(Response.StatusCode);
        return RedirectToPage("../User/UserProfile", loginContent);
    }
}
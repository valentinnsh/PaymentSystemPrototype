using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Auth;
[IgnoreAntiforgeryToken]
public class SignUp : PageModel
{
    [BindProperty]
    public SignUpData signUpData { get; set; }
    public string Message = "";
    public async Task<PageResult> OnGet(string msg)
    {
        Message += msg;
        return Page();
    }
    public async Task<ActionResult> OnPost([FromServices] IAuthService authService, [FromForm] SignUpData signUpData)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var result = await authService.SignUpAsync(signUpData);
        return result switch
        {
            true => RedirectToPage("LogIn"),
            false => RedirectToPage("SignUp", new {msg = "Email is already in use."})
        };
    }
}
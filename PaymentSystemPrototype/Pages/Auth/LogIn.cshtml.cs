using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Auth;

[IgnoreAntiforgeryToken]
public class LogIn : PageModel
{
    [BindProperty]
    public LogInData? LoginContent { get; set; }
    
    public string Message = "";
    public async Task<PageResult> OnGet(string msg)
    {
        Message += msg;
        return Page();
    }
    
    public async Task<ActionResult> OnPost([FromServices] IAuthService authService, 
        [FromServices] IUserOperationsService userOperationsService, [FromForm, FromBody] LogInData loginContent)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        bool loginResult;
        try
        {
            var user = await userOperationsService.FindByEmailAsync(loginContent.Email) ?? throw new UserNotFoundException();
            if(await userOperationsService.IsUserBlocked(user.Id))
            {
                return RedirectToPage("LogIn",
                new {msg = "You are blocked by Administrator"});
            }
            loginResult = await authService.LogInAsync(loginContent.Email, loginContent.Password, HttpContext);
        }
        catch (UserNotFoundException)
        {
            return RedirectToPage("LogIn", new {msg = "Email not found. Consider Signing Up first."});
        }
        return loginResult switch
        {
            true => RedirectToPage("../UserPages/UserProfile", loginContent),
            false => RedirectToPage("LogIn",
                new {msg = "Wrong Password"}),
        };
    }
}
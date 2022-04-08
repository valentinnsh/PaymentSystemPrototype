using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

public class ModifyData : PageModel
{
    public void OnGet()
    {
    }
    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, [FromForm] 
        SignUpData newData)
    {
        var result = await userOperationsService.ModifyUserAsync(newData, User.Identity.Name);
        return result switch
        {
            true => RedirectToPage("../Auth/LogIn"),
            false => RedirectToPage("../Auth/WelcomeRazor", new {msg = $"{result}"})
        };
    }
}
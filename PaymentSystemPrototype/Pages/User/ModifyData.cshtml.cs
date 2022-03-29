using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.User;

public class ModifyData : PageModel
{
    public void OnGet(string previousEmail)
    {
    }
    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, [FromForm] 
        SignUpData newData)
    {
        var result = await userOperationsService.ModifyUser(newData, User.Identity.Name);
        if (result is HttpStatusCode.OK)
        {
            return RedirectToPage("../Auth/LogIn");
        }
        return RedirectToPage("../Auth/WelcomeRazor", new {msg = $"{(int)result}"});
    }
}
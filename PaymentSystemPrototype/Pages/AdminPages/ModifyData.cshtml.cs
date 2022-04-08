using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class ModifyData : PageModel
{
    public string UserEmail = "";
    public static string PreviousEmail = "";
    public void OnGet(string email)
    {
        UserEmail = PreviousEmail = email;
    }
    
    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, [FromForm] 
        SignUpData newData)
    {
        
        var result = await userOperationsService.ModifyUserAsync(newData, PreviousEmail);
        return result switch
        {
            true => RedirectToPage("UserList"),
            false => throw new UserNotFoundException()
        };
    }
}
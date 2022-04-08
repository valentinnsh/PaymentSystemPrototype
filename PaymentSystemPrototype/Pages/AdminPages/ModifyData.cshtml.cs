using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;
[Authorize(Roles = "Admin")]
public class ModifyData : PageModel
{
    public string UserEmail = "";
    public UserRecord? PresentedUser;
    public static string PreviousEmail = "";
    public void OnGet([FromServices] IUserOperationsService userOperationsService, string email)
    {
        UserEmail = PreviousEmail = email;
        PresentedUser = userOperationsService.FindByEmail(email);
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
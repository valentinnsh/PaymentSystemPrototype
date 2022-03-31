using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class AssignRole : PageModel
{
    public Roles AssignedRole { get; set; }
    public static string UserEmail { get; set; }
    public void OnGet(string email)
    {
        UserEmail = email;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, [FromForm] int assignedRole)
    {
        
        var result = await userOperationsService.SetRole(UserEmail, (Roles)assignedRole-1);
        if (result is HttpStatusCode.OK)
        {
            return RedirectToPage("UserList");
        }
        return RedirectToPage("../Auth/WelcomeRazor", new {msg = $"Error: {(int)result + UserEmail}"});
    }
}
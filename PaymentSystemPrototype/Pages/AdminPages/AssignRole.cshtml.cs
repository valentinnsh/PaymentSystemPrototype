using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

[Authorize(Roles = "Admin")]
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
        
        var result = await userOperationsService.SetRoleAsync(UserEmail, (Roles)assignedRole-1);
        return result switch
        {
            true => RedirectToPage("UserList"),
            false => throw new UserNotFoundException()
        };
    }
}
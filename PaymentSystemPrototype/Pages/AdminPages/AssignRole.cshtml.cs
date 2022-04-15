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
    public static int UserId { get; set; }
    public void OnGet(int id)
    {
        UserId = id;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, [FromForm] int assignedRole)
    {
        
        var result = await userOperationsService.SetRoleAsync(UserId, (Roles)assignedRole-1);
        return result switch
        {
            true => RedirectToPage("UserList"),
            false => throw new UserNotFoundException()
        };
    }
}
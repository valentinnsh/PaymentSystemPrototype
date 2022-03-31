using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class AssignRole : PageModel
{
    [BindProperty]
    public Roles AssignedRole { get; set; }
    [BindProperty]
    public string UserEmail { get; set; }
    public void OnGet(string userEmail)
    {
        UserEmail = userEmail;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService)
    {
        var result = await userOperationsService.SetRole(UserEmail, AssignedRole);
        if (result is HttpStatusCode.OK)
        {
            return RedirectToPage("UserList");
        }
        return RedirectToPage("../Auth/WelcomeRazor", new {msg = $"Error: {(int)result}"});
    }
}
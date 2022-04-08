using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class InvertBlockStatus : PageModel
{
    public string IncomingMessage = "";
    public int UserId;
    public void OnGet(int userId, string statusEmail)
    {
        UserId = userId;
        IncomingMessage = statusEmail;
    }
    public async Task<IActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, int userId)
    {
        var result = userOperationsService.RevertBlockStatusAsync(userId).Result;
        return result switch
        {
            true => RedirectToPage("UserList"),
            false => throw new UserNotFoundException()
        };
    }
}
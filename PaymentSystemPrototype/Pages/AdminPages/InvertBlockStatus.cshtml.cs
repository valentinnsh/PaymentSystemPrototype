using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class InvertBlockStatus : PageModel
{
    public string UserEmail = "";
    public int UserId;
    public void OnGet(int userId, string statusEmail)
    {
        UserId = userId;
        UserEmail = statusEmail;
    }
    public async Task<IActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, int userId)
    {
        var result = userOperationsService.RevertBlockStatus(userId).Result;
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("UserList"),
            _ => throw new Exception("Unexpected Error")
        };
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.FundsPages;

public class AddFunds : PageModel
{
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public async Task<IActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, string userEmail,
        int amountAdded)
    {
        var result = userOperationsService.AddFunds(userEmail, amountAdded).Result;
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("AddFunds", new {msg = $"Done {userEmail} {amountAdded}"}),
            HttpStatusCode.NotFound => RedirectToPage("AddFunds", new {msg = "UserNotFound"}),
            _ => RedirectToPage("AddFunds", new {msg = "Unknown error"})
        };
    }
}
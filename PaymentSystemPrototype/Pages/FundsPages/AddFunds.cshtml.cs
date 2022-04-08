using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.FundsPages;

[Authorize(Roles = "FundsManager")]
public class AddFunds : PageModel
{
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public Task<IActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, string userEmail,
        int amountAdded)
    {
        var result = userOperationsService.AddFundsAsync(userEmail, amountAdded).Result;
        return Task.FromResult<IActionResult>(result switch
        {
            true => RedirectToPage("AddFunds", new {msg = $"Done adding {amountAdded} to {userEmail}"}),
            false => RedirectToPage("AddFunds", new {msg = "User was not found"})
        });
    }
}
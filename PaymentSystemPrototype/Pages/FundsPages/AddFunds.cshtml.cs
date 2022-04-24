using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.FundsPages;

[Authorize(Roles = "FundsManager")]
public class AddFunds : AlteredPageModel
{
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public Task<IActionResult> OnPost([FromServices] ITransferOperationsService transferOperations, string userEmail,
        int amountAdded)
    {
        var result =transferOperations.AddFundsAsync(userEmail, amountAdded, GetUsersId()).Result;
        return Task.FromResult<IActionResult>(result switch
        {
            true => RedirectToPage("AddFunds", new {msg = $"Done adding {amountAdded} to {userEmail}"}),
            false => RedirectToPage("AddFunds", new {msg = "User was not found"})
        });
    }
}
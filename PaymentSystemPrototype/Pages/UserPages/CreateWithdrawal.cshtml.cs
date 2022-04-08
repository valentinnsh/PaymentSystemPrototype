using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

[Authorize(Roles = "User")]
public class CreateWithdrawal : PageModel
{
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService,
        [FromServices] ITransferOperationsService transferOperationsService, [FromForm] TransferData transferData)
    {
        transferData.Amount *= -1;
        var result = transferOperationsService.CreateTransferRequestAsync(transferData, User.Identity.Name).Result;
        return result switch
        {
            true => RedirectToPage("/UserPages/UserProfile"),
            false => RedirectToPage("CreateWithdrawal", new {msg = "Balance is too low"})
        };
    }
}
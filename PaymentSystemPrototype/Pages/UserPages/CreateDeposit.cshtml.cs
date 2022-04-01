using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

public class CreateDeposit : PageModel
{
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService,
        [FromServices] ITransferOperationsService transferOperationsService, [FromForm] TransferData transferData)
    {
        var result = transferOperationsService.CreateTransferRequest(transferData, User.Identity.Name).Result;
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("/UserPages/UserProfile"),
            _ => RedirectToPage("CreateDeposit", new {msg = "Unknown Error, try again"})
        };
    }
}
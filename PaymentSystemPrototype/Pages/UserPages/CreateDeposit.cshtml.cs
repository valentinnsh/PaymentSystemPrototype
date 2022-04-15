using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;
[Authorize(Roles = "User")]
public class CreateDeposit : AlteredPageModel
{
    [BindProperty]
    public TransferData transferData { get; set; }
    public string Message = "";
    public void OnGet(string msg)
    {
        Message = msg;
    }

    public async Task<ActionResult> OnPost([FromServices] IUserOperationsService userOperationsService,
        [FromServices] ITransferOperationsService transferOperationsService, [FromForm] WithdrawalData transferData)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var result = transferOperationsService.CreateTransferRequestAsync(transferData, GetUsersId()).Result;
        return result switch
        {
            true => RedirectToPage("/UserPages/UserProfile"),
            _ => RedirectToPage("CreateDeposit", new {msg = "Unknown Error, try again"})
        };
    }
}
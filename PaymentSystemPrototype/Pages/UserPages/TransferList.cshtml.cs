using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

[Authorize(Roles = "User")]
public class TransferList : AlteredPageModel
{
    public IQueryable<TransferRecord> UserTransfers;
    public IQueryable<UserRecord> UserList; 
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService,
        [FromServices] IUserOperationsService userOperationsService)
    {
        UserTransfers = transferOperationsService.GetTransfersForUser(GetUsersId());
        UserList =  userOperationsService.GetUsers();
    }

    public async Task<IActionResult> OnPostCancel([FromServices] ITransferOperationsService transferOperationsService,
        int transferId)
    {
        var result = await transferOperationsService.CancelTransferAsync(transferId);
        return result switch
        {
            true => RedirectToPage("TransferList"),
            false => throw new Exception("Unexpected error")
        };
    }
}
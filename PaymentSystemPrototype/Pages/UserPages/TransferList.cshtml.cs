using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

[Authorize(Roles = "User")]
public class TransferList : PageModel
{
    public IList<TransferRecord> UserTransfers;
    public IList<UserRecord> UserList; 
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService,
        [FromServices] IUserOperationsService userOperationsService)
    {
        UserTransfers = transferOperationsService.GetTransfersForUser(User.Identity.Name);
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
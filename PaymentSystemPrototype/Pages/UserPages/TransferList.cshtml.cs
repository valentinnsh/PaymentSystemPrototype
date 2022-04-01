using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

public class TransferList : PageModel
{
    public List<TransferRecord> UserTransfers;
    public List<UserRecord> UserList; 
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService,
        [FromServices] IUserOperationsService userOperationsService)
    {
        UserTransfers = transferOperationsService.GetTransfersForUser(User.Identity.Name);
        UserList = userOperationsService.GetUsers();
    }

    public async Task<IActionResult> OnPostCancel([FromServices] ITransferOperationsService transferOperationsService,
        int transferId)
    {
        var result = await transferOperationsService.CancelTransfer(transferId);
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("TransferList"),
            _ => throw new Exception("Unexpected error")
        };
    }
}
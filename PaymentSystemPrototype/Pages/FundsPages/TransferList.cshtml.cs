using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.FundsPages;

public class TransferList : PageModel
{
    public List<TransferRecord> ListRequests;
    public List<UserRecord> ListUsers;
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService, 
        [FromServices] IUserOperationsService userOperationsService)
    {
        // It would make sense if Funds Manager wants to see for unreviewed transfers first  
        ListRequests = transferOperationsService.GetTransfersUnreviewedFirst();
        ListUsers = userOperationsService.GetUsers();
    }

    public async Task<IActionResult> OnPostReview([FromServices] ITransferOperationsService transferOperationsService,
        int transferId, int status)
    {
        var result = await transferOperationsService.SetStatus((ReviewStatus)status, User.Identity.Name, transferId);
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("TransferList"),
            _ => throw new Exception("Unexpected error")
        };
    }
}

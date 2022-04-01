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
    public List<BalanceRecord> ListBalances;
    public string Message = "";
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService, 
        [FromServices] IUserOperationsService userOperationsService, string msg = "")
    {
        // It would make sense if Funds Manager wants to see for unreviewed transfers first  
        ListRequests = transferOperationsService.GetTransfersUnreviewedFirst();
        ListUsers = userOperationsService.GetUsers();
        ListBalances = userOperationsService.GetBalances();
        Message = msg;
    }

    public async Task<IActionResult> OnPostReview([FromServices] ITransferOperationsService transferOperationsService,
        int transferId, int status)
    {
        var result = await transferOperationsService.SetStatus((ReviewStatus)status, User.Identity.Name, transferId);
        return result switch
        {
            HttpStatusCode.OK => RedirectToPage("TransferList"),
            HttpStatusCode.Forbidden => RedirectToPage("TransferList", new {msg = "Rejected"}),
            HttpStatusCode.NotFound => RedirectToPage("TransferList", new {msg = "User was not found"}),
            _ => throw new Exception("Unexpected error")
        };
    }
}

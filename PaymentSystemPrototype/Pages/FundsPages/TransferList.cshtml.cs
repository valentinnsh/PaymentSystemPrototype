using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.FundsPages;

[Authorize(Roles = "FundsManager, Admin")]
public class TransferList : AlteredPageModel
{
    public IQueryable<TransferRecord> ListRequests;
    public IQueryable<UserRecord> ListUsers;
    public IQueryable<BalanceRecord> ListBalances;
    public string Message = "";
    public void OnGet([FromServices] ITransferOperationsService transferOperationsService, 
        [FromServices] IUserOperationsService userOperationsService, string msg = "")
    {
        // It would make sense if Funds Manager wants to see for unreviewed transfers first  
        ListRequests = transferOperationsService.GetTransfersUnreviewedFirst();
        ListUsers =  userOperationsService.GetUsers();
        ListBalances =  userOperationsService.GetBalances();
        Message = msg;
    }

    public async Task<IActionResult> OnPostReview([FromServices] ITransferOperationsService transferOperationsService,
        int transferId, int status)
    {
        var result = await transferOperationsService.SetStatusAsync((ReviewStatus)status, GetUsersId(), transferId);
        return result switch
        {
            true => RedirectToPage("TransferList"),
            false => RedirectToPage("TransferList", new {msg = "Rejected"}),
        };
    }
}

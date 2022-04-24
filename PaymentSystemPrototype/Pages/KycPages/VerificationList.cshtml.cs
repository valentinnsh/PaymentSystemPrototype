using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.KycPages;
[Authorize(Roles = "KycManager, Admin")]
public class VerificationList : AlteredPageModel
{
    public IList<UserRecord> ListUsers;
    public IList<VereficationRecord> ListVerrifivcations;
    public void OnGet([FromServices] IUserOperationsService userOperationsService, [FromServices] IKycService kycService)
    {
        ListUsers = userOperationsService.GetUsers();
        ListVerrifivcations = kycService.GetVerificationRequests();
    }

    public async Task<IActionResult> OnPostReview([FromServices] IKycService kycService, int userId, int status)
    {
        await kycService.UpdateRequestStatusAsync(userId, GetUsersId(), status);
        return RedirectToPage("VerificationList");
    }
}
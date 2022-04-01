using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.KycPages;

public class VerificationList : PageModel
{
    public List<UserRecord> ListUsers;
    public List<VereficationRecord> ListVerrifivcations;
    public void OnGet([FromServices] IUserOperationsService userOperationsService, [FromServices] IKycService kycService)
    {
        ListUsers = userOperationsService.GetUsers();
        ListVerrifivcations = kycService.GetVerificationRequests();
    }

    public async Task<IActionResult> OnPostReview([FromServices] IKycService kycService, string email, int status)
    {
        //return RedirectToPage("../Auth/WelcomeRazor", new {msg =$"{status}"});
        await kycService.UpdateRequestStatus(email, User.Identity.Name, status);
        return RedirectToPage("VerificationList");
    }
}
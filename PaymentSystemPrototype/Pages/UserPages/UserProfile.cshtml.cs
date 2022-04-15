using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;
[Authorize]
public class UserProfile : AlteredPageModel
{
    public UserRecord? PresentedUser = new UserRecord();
    public BalanceRecord? UserBalance = new BalanceRecord();
    public Roles UserRole;
    public async Task OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        PresentedUser = await userOperationsService.FindUserByIdAsync(GetUsersId());
        if (PresentedUser != null) UserBalance = await userOperationsService.GetUserBalanceAsync(PresentedUser.Id);
        UserRole = userOperationsService.GetUserRole(User.Identity.Name);
    }
    
    public async Task<IActionResult> OnPostModifyUser()
    {
        return RedirectToPage("ModifyData", new {previousEmail = User.Identity!.Name});
    }
    public async Task<IActionResult> OnPostRequestKYCVerification([FromServices] IKycService kycService)
    {
        await kycService.CreateVerificationRequestAsync(GetUsersId());
        return RedirectToPage("UserProfile");
    }
    
    public async Task<IActionResult> OnPostCreateDeposit([FromServices] IUserOperationsService userOperationsService)
    {
       return RedirectToPage("CreateDeposit");
    }
    
}
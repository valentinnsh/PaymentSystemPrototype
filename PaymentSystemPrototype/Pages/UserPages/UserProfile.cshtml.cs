using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;
[Authorize]
public class UserProfile : PageModel
{
    public UserRecord? PresentedUser = new UserRecord();
    public BalanceRecord? UserBalance = new BalanceRecord();
    public Roles UserRole;
    public void OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        PresentedUser = userOperationsService.FindByEmail(User.Identity.Name);
        UserBalance = userOperationsService.GetUserBalance(User.Identity.Name);
        UserRole = userOperationsService.GetUserRole(User.Identity.Name);
    }

    public async Task<IActionResult> OnPostLogOut([FromServices] IAuthService authService)
    {
        await authService.LogOutAsync(HttpContext);
        return RedirectToPage("../Auth/LogIn");
    }
    public async Task<IActionResult> OnPostModifyUser()
    {
        return RedirectToPage("ModifyData", new {previousEmail = User.Identity!.Name});
    }
    public async Task<IActionResult> OnPostRequestKYCVerification([FromServices] IKycService kycService)
    {
        await kycService.CreateVerificationRequest(User.Identity.Name);
        return RedirectToPage("UserProfile");
    }
    
    public async Task<IActionResult> OnPostCreateDeposit([FromServices] IUserOperationsService userOperationsService)
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }
    public async Task<IActionResult> OnPostCreateWithdrawal()
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }

    public async Task<IActionResult> OnPostListUsers()
    {
        return RedirectToPage("UserList");
    }
}
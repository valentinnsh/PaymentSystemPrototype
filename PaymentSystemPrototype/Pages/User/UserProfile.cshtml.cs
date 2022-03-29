using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.User;
[Authorize]
public class UserProfile : PageModel
{
    public UserRecord? PresentedUser = new UserRecord();
    
    public void OnGet([FromServices] IUserOperationsService userOperationsService,LogInData loggedInUser)
    {
        PresentedUser = userOperationsService.FindByEmail(loggedInUser.Email);
    }

    public async Task<IActionResult> OnPostLogOut([FromServices] IAuthService authService)
    {
        await authService.LogOutAsync(HttpContext);
        return RedirectToPage("../Auth/LogIn");
    }
    public async Task<IActionResult> OnPostModifyUser()
    {
        return RedirectToPage("ModifyData", new {previousEmail = User.Identity.Name});
    }
    public async Task<IActionResult> OnPostRequestKYCVerification()
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }
    
    public async Task<IActionResult> OnPostCreateDeposit()
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }
    public async Task<IActionResult> OnPostCreateWithdrawal()
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }
    public async Task<IActionResult> OnPostTransferList()
    {
        return RedirectToPage("../Auth/WelcomeRazor", new {msg ="PAGE IS IN DEVELOPMENT"});
    }
}
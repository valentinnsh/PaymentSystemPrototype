using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;
[Authorize]
public class UserProfile : PageModel
{
    public UserRecord? PresentedUser = new UserRecord();
    public BalanceRecord? UserBalance = new BalanceRecord();
    public Roles UserRole;
    public async Task OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        PresentedUser = await userOperationsService.FindUserByIdAsync(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => 
            x.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new UserNotFoundException()));
        UserBalance = await userOperationsService.GetUserBalanceAsync(User.Identity.Name);
        UserRole = userOperationsService.GetUserRole(User.Identity.Name);
    }
    
    public async Task<IActionResult> OnPostModifyUser()
    {
        return RedirectToPage("ModifyData", new {previousEmail = User.Identity!.Name});
    }
    public async Task<IActionResult> OnPostRequestKYCVerification([FromServices] IKycService kycService)
    {
        await kycService.CreateVerificationRequestAsync(User.Identity.Name);
        return RedirectToPage("UserProfile");
    }
    
    public async Task<IActionResult> OnPostCreateDeposit([FromServices] IUserOperationsService userOperationsService)
    {
       return RedirectToPage("CreateDeposit");
    }
    
}
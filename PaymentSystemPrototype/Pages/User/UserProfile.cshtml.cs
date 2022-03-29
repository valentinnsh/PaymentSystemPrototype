using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.User;
[Authorize]
public class UserProfile : PageModel
{
    public UserRecord? presentedUser;
    
    public void OnGet([FromServices] IUserOperationsService userOperationsService,LogInData loggedInUser)
    {
        presentedUser = userOperationsService.FindByEmail(loggedInUser.Email);
    }

    public async Task<IActionResult> OnPostLogOut([FromServices] IAuthService authService)
    {
        await authService.LogOutAsync(HttpContext);
        return RedirectToPage("../Auth/LogIn");
    }
}
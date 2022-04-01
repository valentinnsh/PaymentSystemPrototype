using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.UserPages;

public class LogOut : PageModel
{
    public void OnGet()
    {
        
    }
    public async Task<IActionResult> OnPost([FromServices] IAuthService authService)
    {
        await authService.LogOutAsync(HttpContext);
        return RedirectToPage("../Auth/LogIn");
    }
}
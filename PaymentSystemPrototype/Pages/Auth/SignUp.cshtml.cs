using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Auth;
[IgnoreAntiforgeryToken]
public class SignUp : PageModel
{
    public  async Task<PageResult> OnGet()
    {
        return Page();
    }
    public async Task<ActionResult> OnPost([FromServices] IAuthService authService, [FromForm] SignUpData signUpData)
    {
        if (signUpData.FirstName == null)
            throw new Exception("SignUpData is bad");
        var result = await authService.SignUpAsync(signUpData);
        if (result is HttpStatusCode.OK or HttpStatusCode.Conflict)
        {
            return RedirectToPage("../User/UserProfile");
        }
        return StatusCode((int)result);
    }
}
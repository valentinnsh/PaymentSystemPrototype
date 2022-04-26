using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;

namespace PaymentSystemPrototype.Pages;

public class AlteredPageModel : PageModel
{
    public int GetUsersId() =>
        Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new UserNotFoundException());
}
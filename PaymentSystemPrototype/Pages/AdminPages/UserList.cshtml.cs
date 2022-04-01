using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Profiles;

public class UserList : PageModel
{
    public List<UserRecord> ListUsers = new List<UserRecord>();
    public void OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        ListUsers = userOperationsService.GetUsers();
    }
}
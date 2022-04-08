using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Profiles;

public class UserList : PageModel
{
    public IList<UserRecord> ListUsers = new List<UserRecord>();
    public IList<RoleRecord> ListRoles = new List<RoleRecord>();
    public IList<UserRoleRecord> ListUserRoles = new List<UserRoleRecord>();
    public void OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        ListUsers = userOperationsService.GetUsers();
        ListRoles = userOperationsService.GetRoles();
        ListUserRoles = userOperationsService.GetUserRoles();
    }
}
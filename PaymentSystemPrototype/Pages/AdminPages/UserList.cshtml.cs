using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.Profiles;
[Authorize(Roles = "Admin")]
public class UserList : PageModel
{
    public IQueryable<UserRecord> ListUsers;
    public IQueryable<RoleRecord> ListRoles;
    public IQueryable<UserRoleRecord> ListUserRoles;
    public void OnGet([FromServices] IUserOperationsService userOperationsService)
    {
        ListUsers = userOperationsService.GetUsers();
        ListRoles = userOperationsService.GetRoles();
        ListUserRoles = userOperationsService.GetUserRoles();
    }
}
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Pages.AdminPages;

public class RevertBLockData
{
    public string IncomingMessage = "Are you shure you want to ";
    public int UserId;
    public bool BlockStatus;
}
[Authorize(Roles = "Admin")]
public class InvertBlockStatus : PageModel
{
    [BindProperty]
    public RevertBLockData BlockData { get; set; }
    
    public string IncomingMessage = "";
    public int UserId;
    
    public void OnGet(int userId, string email, bool status)
    {
        BlockData = new RevertBLockData { UserId = userId, BlockStatus = status};
        if (status)
        {
            BlockData.IncomingMessage += $"block user {email}?";
        }
        else
        {
            BlockData.IncomingMessage += $"unblock user {email}?"; 
        }
    }
    public async Task<IActionResult> OnPost([FromServices] IUserOperationsService userOperationsService, int userId, bool status)
    {
        switch (status)
        {
            case true:
                await userOperationsService.BlockUserAsync(userId);
                break;
            case false:
                await userOperationsService.UnblockUserAsync(userId);
                break;
        }
        return RedirectToPage("UserList");
    }
}
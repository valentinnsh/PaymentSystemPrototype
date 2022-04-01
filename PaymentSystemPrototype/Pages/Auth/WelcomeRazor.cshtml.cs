using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Pages.Auth;
[IgnoreAntiforgeryToken]
public class WelcomeRazor : PageModel
{
    public string Message { get; private set; } = "Hi!";

    public void OnGet(string msg)
    {
        Message += msg;
    }
}
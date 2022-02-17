using Microsoft.AspNetCore.Mvc;

namespace PaymentSystemPrototype.Controllers;

[Route("auth")]
public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }
    // GET
    [HttpGet("get-gmails")]
    public object Get()
    {
        return _context.Users.Where(b => b.Email.Contains("gmail")).Select((c) => new
        {
            c.Id,
            c.FirstName,
            c.LastName
        }).ToList();
    }
}
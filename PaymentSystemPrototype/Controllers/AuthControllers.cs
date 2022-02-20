using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSystemPrototype.Controllers;

[Route("auth")]
public class AuthControllers : Controller
{
    private readonly AppDbContext _context;

    public AuthControllers(AppDbContext context)
    {
        _context = context;
    }
    
    [Authorize]
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
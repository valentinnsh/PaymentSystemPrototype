using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class UserOperationsService : IUserOperationsService
{
    private readonly AppDbContext _context;
    public UserOperationsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<UserRecord?> FindByEmail(string userEmail) =>
        Task.FromResult(_context.Users.FirstOrDefault(b =>  b.Email == userEmail));
    
}
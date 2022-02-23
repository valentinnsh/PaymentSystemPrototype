using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class UserOperationsService : IUserOperationsService
{
    private readonly AppDbContext _context;
    public UserOperationsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(UserRecord user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async void DeleteUser(int id)
    {
        _context.Remove(_context.Users.Single(u => u != null && u.Id == 1));
        await _context.SaveChangesAsync();
    }

    public Task<UserRecord?> FindByEmail(string userEmail) =>
        Task.FromResult(_context.Users.FirstOrDefault(b =>  b != null && b.Email == userEmail));

    public Task<UserRecord?> CheckLoginInfo(string userEmail, string userPassword) =>
        Task.FromResult(_context.Users.FirstOrDefault(u => u != null && u.Email == userEmail &&
                                                                                u.Password == userPassword));
}
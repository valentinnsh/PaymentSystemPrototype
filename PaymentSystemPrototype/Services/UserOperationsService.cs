using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype.Exceptions;
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
        await _context.AddAsync(new BalanceRecord
        {
            Amount = 0,
            UserId = user.Id,
            UserRecord = user
        });
        await _context.AddAsync(new UserRoleRecord
        {
            UserId = user.Id,
            RoleId = 1,
            UserRecord = user,
        });
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        _context.Remove(_context.Users.Single(u => u != null && u.Id == id));
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ModifyUserAsync(SignUpData user, string previousEmail)
    {
        var target = _context.Users.SingleOrDefault(u => u.Email == previousEmail);
        if (target != null)
        {
            target.FirstName = user.FirstName;
            target.LastName = user.LastName;
            target.Email = user.Email;
            target.Password = user.Password;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<UserRecord?> FindByEmailAsync(string userEmail) =>
        await _context.Users.FirstOrDefaultAsync(u =>  EF.Functions.ILike(u.Email, $"{userEmail}"));

    public UserRecord? FindUserById(int userId) =>
        _context.Users.FirstOrDefault(u => u.Id == userId);

    public async Task<UserRecord?> FindUserByIdAsync(int userId) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    
    public async Task<UserRecord?> CheckLoginInfoAsync(string userEmail, string userPassword) =>
        await _context.Users.FirstOrDefaultAsync(u =>
            EF.Functions.ILike(u.Email, $"{userEmail}") && u.Password == userPassword);

    public async Task<bool> AddFundsAsync(string userEmail, int amount)
    {
        var balanceUpdate =  _context.Balances.FirstOrDefault(b => 
            EF.Functions.ILike(b.UserRecord.Email, $"{userEmail}"));
        if (balanceUpdate != null)
        {
            balanceUpdate.Amount += amount;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    public async Task<BalanceRecord?> GetUserBalanceAsync(int userId) =>
        await _context.Balances.FirstOrDefaultAsync(b => b.UserRecord.Id == userId);

    public async Task<string> GetUserRoleAsStringAsync(int userId)
    {
        var user =  await FindUserByIdAsync(userId);
        return _context.Roles.FirstOrDefault(
            r => r.Id == _context.UserRoles.FirstOrDefault(ur=>ur.UserId == user.Id).RoleId).Name;
    }

    public Roles GetUserRole(int userId)
    {
        var user = FindUserByIdAsync(userId).Result;
        return (Roles) _context.Roles.FirstOrDefault(
            r => r.Id == _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id).RoleId).Id-1;
    }
    public async Task<bool> SetRoleAsync(int userId, Roles newRole)
    {
        var user = await FindUserByIdAsync(userId);
        if (user != null)
        {
            var roleToSet = await _context.Roles.FirstOrDefaultAsync(b => b.Id == (int) newRole + 1);
            var userRoleToChange = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id);
            if (roleToSet != null)
                if (userRoleToChange != null)
                    userRoleToChange.RoleId = roleToSet.Id;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public IList<UserRecord> GetUsers() =>
        _context.Users.ToList();

    public IList<UserRoleRecord> GetUserRoles() =>
        _context.UserRoles.ToList();

    public IList<RoleRecord> GetRoles() =>
        _context.Roles.ToList();
    
    public IList<BalanceRecord> GetBalances() =>
        _context.Balances.ToList();

    public async Task<bool> IsUserBlocked(int userId)
    {
        var user =  await _context.Users.FirstOrDefaultAsync(b => b.Id == userId) ?? throw new UserNotFoundException();
        return user.Block;
    }
        

    public async Task<bool> RevertBlockStatusAsync(int userId)
    {
        var user = await FindUserByIdAsync(userId);
        if (user == null) return false;
        user.Block = !user.Block;
        await _context.SaveChangesAsync();
        return true;
    }
}
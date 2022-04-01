using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        var newUserBalance = new BalanceRecord();
        newUserBalance.Amount = 0;
        newUserBalance.UserId = user.Id;
        newUserBalance.UserRecord = user;
        await _context.AddAsync(user);
        await _context.AddAsync(newUserBalance);
        await _context.AddAsync(new UserRoleRecord
        {
            UserId = user.Id,
            RoleId = 1,
            UserRecord = user,
        });
        await _context.SaveChangesAsync();
    }

    public async void DeleteUser(int id)
    {
        _context.Remove(_context.Users.Single(u => u != null && u.Id == 1));
        await _context.SaveChangesAsync();
    }

    public async Task<HttpStatusCode> ModifyUser(SignUpData user, string previousEmail)
    {
        var target = _context.Users.SingleOrDefault(u => u.Email == previousEmail);
        if (target != null)
        {
            target.FirstName = user.FirstName;
            target.LastName = user.LastName;
            target.Email = user.Email;
            target.Password = user.Password;
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        return HttpStatusCode.NotFound;
    }
    public UserRecord? FindByEmail(string userEmail) =>
       _context.Users.FirstOrDefault(b =>  b.Email == userEmail);

    public Task<UserRecord?> CheckLoginInfo(string userEmail, string userPassword) =>
        Task.FromResult(_context.Users.FirstOrDefault(u => u != null && u.Email == userEmail &&
                                                                                u.Password == userPassword));

    public async Task<HttpStatusCode> AddFunds(string userEmail, int amount)
    {
        var balanceUpdate =  _context.Balances.FirstOrDefault(b => 
            b.UserRecord.Email == userEmail);
        if (balanceUpdate != null)
        {
            balanceUpdate.Amount += amount;
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        return HttpStatusCode.NotFound;
    }
    public BalanceRecord? GetUserBalance(string userEmail) =>
        _context.Balances.FirstOrDefault(b => b.UserRecord.Email == userEmail);

    public string? GetUserRoleAsString(string userEmail)
    {
        var user = FindByEmail(userEmail);
        return _context.Roles.FirstOrDefault(
            r => r.Id == _context.UserRoles.FirstOrDefault(ur=>ur.UserId == user.Id).RoleId).Name;
    }

    public Roles GetUserRole(string userEmail)
    {
        var user = FindByEmail(userEmail);
        var userRole = _context.UserRoles.FirstOrDefault(ur => user != null && ur.UserId == user.Id);
        return (Roles) _context.Roles.FirstOrDefault(
            r => r.Id == _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id).RoleId).Id-1;
    }
    public async Task<HttpStatusCode> SetRole(string userEmail, Roles newRole)
    {
        var user = FindByEmail(userEmail);
        if (user != null)
        {
            var roleToSet = _context.Roles.FirstOrDefault(b => b.Id == (int) newRole + 1);
            var userRoleToChange = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
            if (roleToSet != null)
                userRoleToChange.RoleId = roleToSet.Id;
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        return HttpStatusCode.NotFound;
    }

    public List<UserRecord> GetUsers() =>
        _context.Users.ToList();

    public List<UserRoleRecord> GetUserRoles() =>
        _context.UserRoles.ToList();

    public List<BalanceRecord> GetBalances() =>
        _context.Balances.ToList();

    public bool IsUserBlocked(string userEmail) =>
        _context.Users.FirstOrDefault(b => b.Email == userEmail).Block;

    public async Task<HttpStatusCode> RevertBlockStatus(int userId)
    {
        var user = _context.Users.FirstOrDefault(b => b.Id == userId);
        if (user != null)
        {
            user.Block = !user.Block;
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        return HttpStatusCode.NotFound;
    }
    
}
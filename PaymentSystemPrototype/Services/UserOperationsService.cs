using System.Data.Entity;
using System.Net;
using Microsoft.EntityFrameworkCore;
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
}
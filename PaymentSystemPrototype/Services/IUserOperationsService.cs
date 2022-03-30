using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    UserRecord? FindByEmail(string userEmail);
    public Task<UserRecord?> CheckLoginInfo(string userEmail, string userPassword);
    Task AddUserAsync(UserRecord user);
    public Task<HttpStatusCode> ModifyUser(SignUpData user, string previousEmail);
    public BalanceRecord? GetUserBalance(string userEmail);
    public Task<HttpStatusCode> AddFunds(string userEmail, int amount);
    void DeleteUser(int id);
}
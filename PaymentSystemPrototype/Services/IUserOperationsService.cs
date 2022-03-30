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
    public string? GetUserRoleAsString(string userEmail);
    public Roles GetUserRole(string userEmail);
    public void SetRole(string userEmail, Roles newRole);
    public List<UserRecord> GetUsers();
    void DeleteUser(int id);
}
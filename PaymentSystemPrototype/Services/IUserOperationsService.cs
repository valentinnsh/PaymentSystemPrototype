using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    UserRecord? FindByEmail(string userEmail);
    public Task<UserRecord?> FindByEmailAsync(string userEmail);
    public Task<UserRecord?> FindUserByIdAsync(int userId);
    public UserRecord? FindUserById(int userId);
    public Task<UserRecord?> CheckLoginInfoAsync(string userEmail, string userPassword);
    Task AddUserAsync(UserRecord user);
    public Task<bool> ModifyUserAsync(SignUpData user, string previousEmail);
    public Task<BalanceRecord?> GetUserBalanceAsync(string userEmail);
    public Task<bool> AddFundsAsync(string userEmail, int amount);
    public string? GetUserRoleAsString(string userEmail);
    public Roles GetUserRole(string userEmail);
    public Task<bool> SetRoleAsync(int userId, Roles newRole);
    public IList<UserRecord> GetUsers();
    public IList<UserRoleRecord> GetUserRoles();
    public IList<RoleRecord> GetRoles();
    public IList<BalanceRecord> GetBalances();
    public Task<bool> IsUserBlocked(string userEmail);
    public Task<bool> RevertBlockStatusAsync(int userId);
    Task DeleteUserAsync(int id);
}
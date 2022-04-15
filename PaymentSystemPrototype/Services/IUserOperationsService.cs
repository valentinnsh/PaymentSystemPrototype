using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    public Task<UserRecord?> FindByEmailAsync(string userEmail);
    public Task<UserRecord?> FindUserByIdAsync(int userId);
    public UserRecord? FindUserById(int userId);
    public Task<UserRecord?> CheckLoginInfoAsync(string userEmail, string userPassword);
    Task AddUserAsync(UserRecord user);
    public Task<bool> ModifyUserAsync(SignUpData user, string previousEmail);
    public Task<BalanceRecord?> GetUserBalanceAsync(int userId);
    public Task<bool> AddFundsAsync(string userEmail, int amount);
    public Task<string> GetUserRoleAsStringAsync(int userId);
    public Roles GetUserRole(int userId);
    public Task<bool> SetRoleAsync(int userId, Roles newRole);
    public IList<UserRecord> GetUsers();
    public IList<UserRoleRecord> GetUserRoles();
    public IList<RoleRecord> GetRoles();
    public IList<BalanceRecord> GetBalances();
    public Task<bool> IsUserBlocked(int userId);
    public Task<bool> RevertBlockStatusAsync(int userId);
    Task DeleteUserAsync(int id);
}
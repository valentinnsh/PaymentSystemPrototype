using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    public Task<UserRecord?> FindByEmailAsync(string userEmail);
    public Task<UserRecord?> FindUserByIdAsync(int userId);
    public Task<UserRecord?> CheckLoginInfoAsync(string userEmail, string userPassword);
    Task AddUserAsync(UserRecord user);
    public Task<bool> ModifyUserAsync(SignUpData user, int userId);
    public Task<BalanceRecord?> GetUserBalanceAsync(int userId);
    public Task<bool> AddFundsAsync(string userEmail, decimal amount);
    public Task<string> GetUserRoleAsStringAsync(int userId);
    public Roles GetUserRole(int userId);
    public Task<bool> SetRoleAsync(int userId, Roles newRole);
    public IList<UserRecord> GetUsers();
    public IList<UserRoleRecord> GetUserRoles();
    public IList<RoleRecord> GetRoles();
    public IList<BalanceRecord> GetBalances();
    public Task<bool> IsUserBlocked(int userId);
    public Task BlockUserAsync(int userId);
    public Task UnblockUserAsync(int userId);
    Task DeleteUserAsync(int id);
}
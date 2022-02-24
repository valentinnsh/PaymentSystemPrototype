using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    UserRecord? FindByEmail(string userEmail);
    public Task<UserRecord?> CheckLoginInfo(string userEmail, string userPassword);
    Task AddUserAsync(UserRecord user);
    void DeleteUser(int id);
}
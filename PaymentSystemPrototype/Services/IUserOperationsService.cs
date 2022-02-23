using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    Task<UserRecord?> FindByEmail(string userEmail);
    public Task<UserRecord?> CheckLoginInfo(string userEmail, string userPassword);
}
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IUserOperationsService
{
    Task<UserRecord?> FindByEmail(string userEmail);
}
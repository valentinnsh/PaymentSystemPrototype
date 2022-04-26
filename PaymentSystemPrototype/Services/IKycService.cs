using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IKycService
{
    public  Task CreateVerificationRequestAsync(int userId);
    public IQueryable<VereficationRecord> GetVerificationRequests();
    public Task UpdateRequestStatusAsync(int userId, int reviewerId, int status);
}
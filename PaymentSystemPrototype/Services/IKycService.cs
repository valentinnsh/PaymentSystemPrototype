using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IKycService
{
    public  Task CreateVerificationRequestAsync(int userId);
    public IList<VereficationRecord> GetVerificationRequests();
    public Task UpdateRequestStatusAsync(int userId, int reviewerId, int status);
}
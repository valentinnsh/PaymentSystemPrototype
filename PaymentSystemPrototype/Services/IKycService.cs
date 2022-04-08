using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IKycService
{
    public  Task CreateVerificationRequestAsync(string userEmail);
    public IList<VereficationRecord> GetVerificationRequests();
    public Task UpdateRequestStatusAsync(string userEmail, string reviewerEmail, int status);
}
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface IKycService
{
    public  Task CreateVerificationRequest(string userEmail);
    public List<VereficationRecord> GetVerificationRequests();
    public Task UpdateRequestStatus(string userEmail, string reviewerEmail, int status);
}
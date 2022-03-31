namespace PaymentSystemPrototype.Services;

public interface IKycService
{
    public  Task CreateVerificationRequest(string userEmail);

}
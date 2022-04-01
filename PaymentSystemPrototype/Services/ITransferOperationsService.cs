using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface ITransferOperationsService
{
    public Task<HttpStatusCode> CreateTransferRequest(TransferData data, string userEmail);

}
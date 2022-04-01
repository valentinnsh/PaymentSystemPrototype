using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface ITransferOperationsService
{
    public Task<HttpStatusCode> CreateTransferRequest(TransferData data, string userEmail);
    public List<TransferRecord> GetTransfersForUser(string userEmail);
    public List<TransferRecord> GetTransfers();
    public Task<HttpStatusCode> CanelTransfer(int transferId);
}
using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface ITransferOperationsService
{
    public Task<bool> CreateTransferRequestAsync(TransferData data, string userEmail);
    public IList<TransferRecord> GetTransfersForUser(string userEmail);
    public IList<TransferRecord> GetTransfers();
    public Task<bool> CancelTransferAsync(int transferId);
    public IList<TransferRecord> GetTransfersUnreviewedFirst();
    public Task<bool> SetStatusAsync(ReviewStatus status, string reviewerEmail, int transferId);
}
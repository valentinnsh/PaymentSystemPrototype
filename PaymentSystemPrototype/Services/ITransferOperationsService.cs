using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface ITransferOperationsService
{
    public Task<bool> CreateTransferRequestAsync(WithdrawalData data, int userId);
    public IList<TransferRecord> GetTransfersForUser(int userId);
    public IList<TransferRecord> GetTransfers();
    public Task<bool> CancelTransferAsync(int transferId);
    public IList<TransferRecord> GetTransfersUnreviewedFirst();
    public Task<bool> SetStatusAsync(ReviewStatus status, int reviewerId, int transferId);
}
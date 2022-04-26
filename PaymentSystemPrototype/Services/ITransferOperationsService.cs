using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public interface ITransferOperationsService
{
    public Task<bool> CreateTransferRequestAsync(WithdrawalData data, int userId);
    public IQueryable<TransferRecord> GetTransfersForUser(int userId);
    public IQueryable<TransferRecord> GetTransfers();
    public Task<bool> CancelTransferAsync(int transferId);
    public IQueryable<TransferRecord> GetTransfersUnreviewedFirst();
    public Task<bool> SetStatusAsync(ReviewStatus status, int reviewerId, int transferId);
    public Task<bool> AddFundsAsync(string userEmail, decimal amount, int reviewerId);
}
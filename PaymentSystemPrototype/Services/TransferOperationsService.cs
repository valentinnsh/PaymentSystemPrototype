using System.Data.Entity;
using System.Net;
using PaymentSystemPrototype.Exceptions;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class TransferOperationsService : ITransferOperationsService
{
    private readonly object _balanceLock = new();
    private readonly IUserOperationsService _userOperationsService;
    private readonly AppDbContext _context;
    public TransferOperationsService(AppDbContext context, IUserOperationsService userOperationsService)
    {
        _context = context;
        _userOperationsService = userOperationsService;
    }

    public async Task<bool> CreateTransferRequestAsync(WithdrawalData data, int userId)
    {
        var user = await _userOperationsService.FindUserByIdAsync(userId) ?? throw new UserNotFoundException();
        var balance = await _userOperationsService.GetUserBalanceAsync(userId);
        if (user == null || balance == null) throw new UserNotFoundException();
        lock (_balanceLock)
        {
            if (balance.Amount + data.Amount < 0)
            {
                return false;
            }

            _context.Transfers.Add(
                new TransferRecord()
                {
                    UserId = user.Id,
                    CardNumber = data.CardNumber,
                    CreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    ConfirmedAt = null,
                    ConfirmedBy = null,
                    Amount = data.Amount,
                    Status = (int) ReviewStatus.InReview
                });
            _context.SaveChanges();
            return true;
        }
    }

    public IList<TransferRecord> GetTransfers() =>
        _context.Transfers.ToList();

    public IList<TransferRecord> GetTransfersUnreviewedFirst() =>
        _context.Transfers.ToList().OrderByDescending(t=>t.Status).ToList();
    public IList<TransferRecord> GetTransfersForUser(int userId)
    {
        var user =  _userOperationsService.FindUserByIdAsync(userId).Result ?? throw new UserNotFoundException();
        return _context.Transfers.Where(t => t.UserId == user.Id).ToList();
    }

    public async Task<bool> CancelTransferAsync(int transferId)
    {
        var result = await _context.Transfers.FindAsync(transferId);
        if (result != null)
        {
            _context.Transfers.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> SetStatusAsync(ReviewStatus status, int reviewerId, int transferId)
    {
        var transfer = await _context.Transfers.FindAsync(transferId);
        if (transfer != null)
        {
            if (status == ReviewStatus.Accepted)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == transfer.UserId);
                var balance = _context.Balances.FirstOrDefault(b => user != null && b.UserId == user.Id);
                if (user == null || balance == null) throw new UserNotFoundException();
                lock (_balanceLock)
                {
                    if (balance.Amount + transfer.Amount > 0)
                    {

                        balance.Amount += transfer.Amount;
                        transfer.Status = (int) status;
                        transfer.ConfirmedAt = DateTime.UtcNow;
                        transfer.ConfirmedBy = reviewerId;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }

            // If review status is Accepted, but balance is too low -> reject automatically
            
            transfer.Status = (int) ReviewStatus.Rejected;
            transfer.ConfirmedAt = DateTime.UtcNow;
            transfer.ConfirmedBy = reviewerId;
            await _context.SaveChangesAsync();
            return false;
        }

        throw new TransferNotFoundException();
    }
}
using System.Data.Entity;
using System.Net;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class TransferOperationsService : ITransferOperationsService
{
    private readonly IUserOperationsService _userOperationsService;
    private readonly AppDbContext _context;
    public TransferOperationsService(AppDbContext context, IUserOperationsService userOperationsService)
    {
        _context = context;
        _userOperationsService = userOperationsService;
    }

    public async Task<HttpStatusCode> CreateTransferRequest(TransferData data, string userEmail)
    {
        var user = _userOperationsService.FindByEmail(userEmail);
        var balance = _userOperationsService.GetUserBalance(userEmail);
        if (user != null)
        {
            if (balance.Amount + data.Amount < 0)
            {
                return HttpStatusCode.Forbidden;
            }
            await _context.Transfers.AddAsync(
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
            await _context.SaveChangesAsync();
        }

        return HttpStatusCode.OK;
    }

    public List<TransferRecord> GetTransfers() =>
        _context.Transfers.ToList();

    public List<TransferRecord> GetTransfersUnreviewedFirst() =>
        _context.Transfers.ToList().OrderByDescending(t=>t.Status).ToList();
    public List<TransferRecord> GetTransfersForUser(string userEmail)
    {
        var user = _userOperationsService.FindByEmail(userEmail);
        if (user != null)
        {
            return _context.Transfers.Where(t => t.UserId == user.Id).ToList();
        }
        return new List<TransferRecord>();
    }

    public async Task<HttpStatusCode> CancelTransfer(int transferId)
    {
        var result = await _context.Transfers.FindAsync(transferId);
        if (result != null)
        {
            _context.Transfers.Remove(result);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        return HttpStatusCode.NotFound;
    }

    public async Task<HttpStatusCode> SetStatus(ReviewStatus status, string reviewerEmail, int transferId)
    {
        var transfer = await _context.Transfers.FindAsync(transferId);
        if (transfer != null)
        {
            if (status == ReviewStatus.Accepted)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == transfer.UserId);
                var balance = _context.Balances.FirstOrDefault(b => user != null && b.UserId == user.Id);
                if (balance!.Amount + transfer.Amount > 0 )
                {
                
                    balance.Amount += transfer.Amount;
                    transfer.Status = (int) status;
                    transfer.ConfirmedAt = DateTime.UtcNow;
                    transfer.ConfirmedBy = _userOperationsService.FindByEmail(reviewerEmail)?.Id;
                    await _context.SaveChangesAsync();
                    return HttpStatusCode.OK;
                }
            }
            else
            {
                return HttpStatusCode.Forbidden;
            }
            transfer.Status = (int) ReviewStatus.Rejected;
            transfer.ConfirmedAt = DateTime.UtcNow;
            transfer.ConfirmedBy = _userOperationsService.FindByEmail(reviewerEmail)?.Id;
            await _context.SaveChangesAsync();
            return HttpStatusCode.Forbidden;
        }

        return HttpStatusCode.NotFound;
    }
}
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
                    Amount = data.Amount
                });
            await _context.SaveChangesAsync();
        }

        return HttpStatusCode.OK;
    }
}
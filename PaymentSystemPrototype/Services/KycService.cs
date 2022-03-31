using System.Net;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Services;

public class KycService : IKycService
{
    private readonly IUserOperationsService _userOperationsService;
    private readonly AppDbContext _context;
    public KycService(AppDbContext context, IUserOperationsService userOperationsService)
    {
        _context = context;
        _userOperationsService = userOperationsService;
    }

    public async Task CreateVerificationRequest(string userEmail)
    {
        var user = _userOperationsService.FindByEmail(userEmail);
        if (user != null && _context.Verefications.FirstOrDefault(v => v.UserId == user.Id) == null)
        {
            await _context.Verefications.AddAsync(
                new VereficationRecord
                {
                    UserId = user.Id,
                    Status = 2,
                    LastChangeDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
                    Reviewer = null
                });
            await _context.SaveChangesAsync();
        }
    }

    public List<VereficationRecord> GetVerificationRequests() =>
        _context.Verefications.ToList();

    public async Task UpdateRequestStatus(string userEmail, string reviewerEmail, int status)
    {
        var user = _userOperationsService.FindByEmail(userEmail);
        var request = _context.Verefications.FirstOrDefault(v => v.UserId == user.Id);
        if (request != null)
        {
            request.Reviewer = reviewerEmail;
            request.Status = status;
            request.LastChangeDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            await _context.SaveChangesAsync();
        }
    }
    
}
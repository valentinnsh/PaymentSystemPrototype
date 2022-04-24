using System.Net;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentSystemPrototype.Exceptions;
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

    public async Task CreateVerificationRequestAsync(int userId)
    {
        var user = await _userOperationsService.FindUserByIdAsync(userId) ?? throw new UserNotFoundException();

        if (_context.Verefications.FirstOrDefault(v => v.UserId == user.Id) == null)
        {
            await _context.Verefications.AddAsync(
                new VereficationRecord
                {
                    UserId = user.Id,
                    Status = (int) ReviewStatus.InReview,
                    LastChangeDate = DateTime.UtcNow,
                    Reviewer = null
                });
            await _context.SaveChangesAsync();
        }
        else
            throw new RequestAlreadyExistsException();
    }

    public IList<VereficationRecord> GetVerificationRequests() =>
        _context.Verefications.ToList();

    public async Task UpdateRequestStatusAsync(int userId, string reviewerEmail, int status)
    {
        var user = await _userOperationsService.FindUserByIdAsync(userId) ?? throw new UserNotFoundException();
        var request = _context.Verefications.FirstOrDefault(v => v.UserId == user.Id) 
                      ?? throw new RequestNotFoundException();
        
        request.Reviewer = reviewerEmail;
        request.Status = status;
        request.LastChangeDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        
    }
    
}
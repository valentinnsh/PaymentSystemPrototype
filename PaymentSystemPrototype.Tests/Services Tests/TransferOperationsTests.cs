using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PaymentSystemPrototype.Models;

namespace PaymentSystemPrototype.Tests.Services_Tests;

[TestFixture]
public class TransferOperationsTests : UnitTestBase
{

    [TestCase("igor@gmail.com",100,7)]
    public async Task AddFundsAsync_Succeeds(string userEmail, decimal amount, int reviewerId) =>
        (await transferOperationsService.AddFundsAsync(userEmail,amount,reviewerId)).Should().Be(true);

    [TestCase(6,100)]
    [TestCase(6,-100)]
    public async Task CreateTransferRequestAsync_Succeeds(int userId, decimal amount)
    {
        var data = new WithdrawalData
        {
            CardNumber = 1234567812345678,
            Amount = amount
        };
        (await transferOperationsService.CreateTransferRequestAsync(data,userId)).Should().Be(true);
    }
    
    [TestCase(6,-101)]
    public async Task CreateTransferRequestAsync_Fails(int userId, decimal amount)
    {
        var data = new WithdrawalData
        {
            CardNumber = 1234567812345678,
            Amount = amount
        };
        (await transferOperationsService.CreateTransferRequestAsync(data,userId)).Should().Be(false);
    }

    [TestCase(7,3)]
    public async Task ConfirmTransfer_Succeeds(int reviewerId, int transferId) =>
        (await transferOperationsService.SetStatusAsync(ReviewStatus.Accepted, reviewerId, transferId)).Should().Be(true);
    
    [TestCase(7,4)]
    public async Task ConfirmTransfer_Fails(int reviewerId, int transferId) =>
        (await transferOperationsService.SetStatusAsync(ReviewStatus.Accepted, reviewerId, transferId)).Should().Be(false);

}
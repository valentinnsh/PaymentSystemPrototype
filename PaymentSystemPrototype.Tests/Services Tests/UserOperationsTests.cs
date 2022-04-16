using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests.Services_Tests;

public class UserOperationsTests : TestBase
{

    [TestCase(1, "admin@gmail.com")]
    public async Task GetUserById_Returns_CorrectUser(int userId, string expectedEmail)
    {
        var result = await userOperationsService.FindUserByIdAsync(userId);
        result?.Email.Should().Be(expectedEmail);
    }

    [TestCase("user1@gmail.com", "user1", 4)]
    public async Task CheckLoginInfo_Succeeds(string userEmail, string password, int expectedUserId)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result?.Id.Should().Be(expectedUserId);
    }

    [TestCase("UsEr1@gMail.com", "user1", 4)]
    public async Task CheckLoginInfo_Succeeds_EmailInMixedCase(string userEmail, string password, int expectedUserId)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result?.Id.Should().Be(expectedUserId);
    }

    [TestCase("user1@gmail.com", "WrongPassword", null)]
    public async Task CheckLoginInfo_Fails(string userEmail, string password, int expectedUserId)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result?.Id.Should().Be(expectedUserId);
    }

    [TestCase(1,"Admin")]
    public async Task GetUserRole_Succeeds(int userId, string expectedRole) =>
        (await userOperationsService.GetUserRoleAsStringAsync(userId)).Should().Be(expectedRole);

}
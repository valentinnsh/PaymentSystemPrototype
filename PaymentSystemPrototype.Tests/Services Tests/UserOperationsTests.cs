using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PaymentSystemPrototype.Models;
using PaymentSystemPrototype.Services;

namespace PaymentSystemPrototype.Tests.Services_Tests;

public class UserOperationsTests : TestBase
{

    [TestCase(6, "igor@gmail.com")]
    public async Task GetUserById_Returns_CorrectUser(int userId, string expectedEmail)
    {
        var result = await userOperationsService.FindUserByIdAsync(userId);
        result.Should().NotBeNull();
        result?.Email.Should().Be(expectedEmail);
    }

    [TestCase("igor@gmail.com", "igor", 4)]
    public async Task CheckLoginInfo_Succeeds(string userEmail, string password, int expectedUserId)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result.Should().NotBeNull();
        result?.Id.Should().Be(expectedUserId);
    }

    [TestCase("IgOr@gMail.com", "igor", 4)]
    public async Task CheckLoginInfo_Succeeds_EmailInMixedCase(string userEmail, string password, int expectedUserId)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result.Should().NotBeNull();
        result?.Id.Should().Be(expectedUserId);
    }

    [TestCase("igor@gmail.com", "WrongPassword")]
    public async Task CheckLoginInfo_Fails(string userEmail, string password)
    {
        var result = await userOperationsService.CheckLoginInfoAsync(userEmail, password);
        result.Should().BeNull();
    }

    [TestCase(6, "User")]
    public void GetUserRoleAsString_Succeeds(int userId, string expectedRole) =>
        userOperationsService.GetUserRoleAsString(userId).Should().Be(expectedRole);

    [TestCase(6,Roles.FundsManager)]
    public async Task SetUserRole_Succeeds(int userId, Roles newRole) =>
        (await userOperationsService.SetRoleAsync(userId, newRole)).Should().Be(true);
    
    [TestCase(0,Roles.FundsManager)]
    public async Task SetUserRoleForNonExistentUser_Fails(int userId, Roles newRole) =>
        (await userOperationsService.SetRoleAsync(userId, newRole)).Should().Be(false);
    
    
}
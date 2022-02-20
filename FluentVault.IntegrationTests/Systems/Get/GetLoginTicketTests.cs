using System;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;

public class GetLoginTicketTests
{
    [Fact]
    public async Task GetLoginTicketBuilder_ShouldReturnValidTicketAndGuid_WhenInputIsValid()
    {
        // Arrange
        var v = VaultOptions.Get();

        // Act
        await using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Assert
        vault.Ticket.Should().NotBeEmpty();
        vault.UserId.Should().BePositive();
    }

    [Theory]
    [InlineData("", "database", "username", "")]
    [InlineData("server", "", "username", "")]
    [InlineData("server", "database", "", "")]
    [InlineData(" ", "database", "username", "")]
    [InlineData("server", " ", "username", "")]
    [InlineData("server", "database", " ", "")]
    public async Task GetLoginTicketBuilder_ShouldThrowAnArgumentException_WhenInputIsEmptyOrWhiteSpace(string server, string database, string username, string password)
    {
        await Assert.ThrowsAsync<ArgumentException>(async () => await Vault.SignIn
            .ToVault(server, database)
            .WithCredentials(username, password));
    }

    [Theory]
    [InlineData(null, "database", "username", "")]
    [InlineData("server", null, "username", "")]
    [InlineData("server", "database", null, "")]
    [InlineData("server", "database", "username", null)]
    public async Task GetLoginTicketBuilder_ShouldThrowAnArgumentNullException_WhenInputIsNull(string server, string database, string username, string password)
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Vault.SignIn
            .ToVault(server, database)
            .WithCredentials(username, password));
    }
}

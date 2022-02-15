using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class GetLoginTicketTests : BaseTest
{
    [Fact]
    public async Task GetLoginTicketBuilder_ShouldReturnValidTicketAndGuid_WhenInputIsValid()
    {
        // Act
        var server = Configuration.GetValue<string>(nameof(VaultOptions.Server));
        var database = Configuration.GetValue<string>(nameof(VaultOptions.Database));
        var username = Configuration.GetValue<string>(nameof(VaultOptions.Username));
        var password = Configuration.GetValue<string>(nameof(VaultOptions.Password));

        using var vault = await Vault.SignIn
            .ToVault(server, database)
            .WithCredentials(username, password);

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

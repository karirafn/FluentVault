using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems;

public class SignInHandlerShould
{
    [Fact]
    public async Task SignIn()
    {
        // Arrange
        VaultOptions options = new()
        {
            Server = "ska-vaultpro",
            Database = "skaginn3xpro",
            Username = "admin",
            Password = ""
        };
        IHttpClientFactory factory = new VaultHttpClientFactory();
        VaultRequestService service = new(factory);
        SignInHandler handler = new(service);
        SignInCommand command = new(options);

        // Act
        VaultSessionCredentials session = await handler.Handle(command, default);

        // Assert
        session.Ticket.Should().NotBeEmpty();
        session.UserId.Should().BeGreaterThan(0);
    }
}

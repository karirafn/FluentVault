using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Microsoft.Extensions.Options;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;

public class SignInHandlerShould
{
    [Fact]
    public async Task SignIn()
    {
        // Arrange
        IOptions<VaultOptions> vaultOptions = new VaultOptionsFixture().Create();
        SignInCommand command = new(vaultOptions.Value);

        await using VaultServiceProvider provider = new();
        IVaultService vaultService = provider.GetRequiredService<IVaultService>();
        SignInHandler sut = new(vaultService);

        // Act
        VaultSessionCredentials session = await sut.Handle(command, default);

        // Assert
        session.Ticket.Should().NotBeEmpty();
        session.UserId.Should().BeGreaterThan(0);
    }
}

using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Domain.SecurityHeader;
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
        SignInCommand command = new();

        VaultServiceProvider provider = new();
        IVaultService vaultService = provider.GetRequiredService<IVaultService>();
        SignInHandler sut = new(vaultService, vaultOptions);

        // Act
        VaultSecurityHeader securityHeader = await sut.Handle(command, default);

        // Assert
        securityHeader.Ticket.Value.Should().NotBeEmpty();
        securityHeader.UserId.Value.Should().BeGreaterThan(0);
    }
}

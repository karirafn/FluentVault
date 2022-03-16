using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems;

public class SignInHandlerShould
{
    [Fact]
    public async Task SignIn()
    {
        // Arrange
        IVaultRequestService service = new VaultRequestServiceFixture().VaultRequestService;
        VaultOptions options = new VaultOptionsFixture().Options;
        SignInCommand command = new(options);
        SignInHandler sut = new(service);

        // Act
        VaultSessionCredentials session = await sut.Handle(command, default);

        // Assert
        session.Ticket.Should().NotBeEmpty();
        session.UserId.Should().BeGreaterThan(0);
    }
}

using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;

public class SignInHandlerShould : IClassFixture<VaultFixture>
{
    private readonly SignInCommand _command;
    private readonly SignInHandler _sut;

    public SignInHandlerShould(VaultFixture fixture)
    {
        _sut = new(fixture.Service);
        _command = new(fixture.Options.Value);
    }

    [Fact]
    public async Task SignIn()
    {
        // Arrange

        // Act
        VaultSessionCredentials session = await _sut.Handle(_command, default);

        // Assert
        session.Ticket.Should().NotBeEmpty();
        session.UserId.Should().BeGreaterThan(0);
    }
}

using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Features;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;

public class SignInHandlerShould : BaseIntegrationTest
{
    private readonly SignInCommand _command = new(_options);
    private readonly SignInHandler _sut = new(_service);

    [Fact]
    public async Task SignIn()
    {
        // Arrange

        // Act
        _session = await _sut.Handle(_command, default);

        // Assert
        _session.Ticket.Should().NotBeEmpty();
        _session.UserId.Should().BeGreaterThan(0);
    }
}

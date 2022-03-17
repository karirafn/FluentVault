using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;

public class SignInHandlerShould : IAsyncLifetime
{
    private readonly IVaultRequestService _service;
    private readonly SignInCommand _command;
    private readonly SignInHandler _sut;
    private readonly VaultOptions _options;
    private VaultSessionCredentials _session;

    public SignInHandlerShould()
    {
        _service = new VaultRequestServiceFixture().VaultRequestService;
        _sut = new(_service);
        _options = new VaultOptionsFixture().Options;
        _command = new(_options);
        _session = new();
    }

    public async Task InitializeAsync() => await Task.CompletedTask;
    async Task IAsyncLifetime.DisposeAsync() => await new SignOutHandler(_service).Handle(new SignOutCommand(_session), default);

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

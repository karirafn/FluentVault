using System.Threading.Tasks;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Features;
public abstract class BaseHandlerTest : IAsyncLifetime
{
    internal static readonly IVaultRequestService _service = new VaultRequestServiceFixture().VaultRequestService;
    protected static readonly VaultOptions _options = new VaultOptionsFixture().Options;

    protected VaultSessionCredentials _session = new();

    public virtual async Task InitializeAsync() => await Task.CompletedTask;
    async Task IAsyncLifetime.DisposeAsync() => await new SignOutHandler(_service).Handle(new SignOutCommand(_session), default);
}

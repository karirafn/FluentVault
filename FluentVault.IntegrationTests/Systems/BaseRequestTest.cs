using System.Threading.Tasks;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems;

public abstract class BaseRequestTest : IAsyncLifetime
{
    protected readonly VaultOptions _v;
    protected Vault _vault;

    public BaseRequestTest() => _v = VaultOptions.Get();

    public async Task InitializeAsync()
        => _vault = await Vault.SignIn
        .ToVault(_v.Server, _v.Database)
        .WithCredentials(_v.Username, _v.Password);

    public async Task DisposeAsync() => await _vault.SignOut();
}

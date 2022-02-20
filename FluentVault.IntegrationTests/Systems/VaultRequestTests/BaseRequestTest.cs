using System.Threading.Tasks;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public abstract class BaseRequestTest : IAsyncLifetime
{
    protected readonly VaultOptions _v;
    protected Vault _vault;

    public BaseRequestTest() => _v = ConfigurationHelper.GetVaultOptions();

    public async Task InitializeAsync()
        => _vault = await Vault.SignIn
        .ToVault(_v.Server, _v.Database)
        .WithCredentials(_v.Username, _v.Password);

    public async Task DisposeAsync() => await _vault.SignOut();
}


using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultServiceProvider : IAsyncDisposable
{
    private readonly ServiceProvider _provider;

    public VaultServiceProvider()
    {
        IOptions<VaultOptions> vaultOptions = new VaultOptionsFixture().Create();
        _provider = new ServiceCollection()
            .AddFluentVault(options =>
            {
                options.Server = vaultOptions.Value.Server;
                options.Database = vaultOptions.Value.Database;
                options.Username = vaultOptions.Value.Username;
                options.Password = vaultOptions.Value.Password;
            })
            .BuildServiceProvider();
    }

    public T GetRequiredService<T>() => _provider.GetRequiredService<T>();

    public async ValueTask DisposeAsync()
    {
        await _provider.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

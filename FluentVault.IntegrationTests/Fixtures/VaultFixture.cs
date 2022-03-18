
using System;

using FluentVault.Common;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultFixture : IDisposable
{
    public VaultFixture()
    {
        Options = new VaultOptionsFixture().Create();
        ServiceProvider provider = new ServiceCollection()
        .AddFluentVault(options =>
        {
            options.Server = Options.Value.Server;
            options.Database = Options.Value.Database;
            options.Username = Options.Value.Username;
            options.Password = Options.Value.Password;
        })
        .BuildServiceProvider();
        Mediator = provider.GetRequiredService<IMediator>();
        Service = provider.GetRequiredService<IVaultService>();
    }

    public IOptions<VaultOptions> Options { get; }
    public IMediator Mediator { get; }
    internal IVaultService Service { get; }

    public void Dispose()
    {
        Service.DisposeAsync().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }
}

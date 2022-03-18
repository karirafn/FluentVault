
using FluentVault.Common;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace FluentVault;

public static class ConfigureServices
{
    public static IServiceCollection AddFluentVault(this IServiceCollection services, Action<VaultOptions> options)
    {
        VaultOptions vaultOptions = new();
        options.Invoke(vaultOptions);

        return services
            .Configure(options)
            .AddHttpClient("Vault", httpClient =>
                {
                    httpClient.BaseAddress = new Uri($@"http://{vaultOptions.Server}/");
                }).Services
            .AddMediatR(typeof(VaultClient).Assembly)
            .AddTransient<IVaultService, VaultService>()
            .AddSingleton<IVaultClient, VaultClient>();
    }
}


using FluentVault.Common;
using FluentVault.RequestBuilders;

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
            .AddRequestBuilders()
            .AddSingleton<IVaultClient, VaultClient>();
    }

    private static IServiceCollection AddRequestBuilders(this IServiceCollection services)
    {
        typeof(VaultClient)
            .Assembly
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IRequestBuilder)))
            .Where(type => !type.IsInterface)
            .ToList()
            .ForEach(type => services.AddSingleton(type.GetInterface($"I{type.Name}")!, type));

        return services;
    }
}


using System.Reflection;

using FluentVault.Common;
using FluentVault.RequestBuilders;
using FluentVault.RequestBuilders.Search;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace FluentVault;

public static class ConfigureServices
{
    public static IServiceCollection AddFluentVault(this IServiceCollection services, Action<VaultOptions> options)
    {
        VaultOptions vaultOptions = new();
        options.Invoke(vaultOptions);
        Assembly assembly = typeof(VaultClient).Assembly;

        return services
            .Configure(options)
            .AddHttpClient("Vault", httpClient =>
                {
                    httpClient.BaseAddress = new Uri($@"http://{vaultOptions.Server}/");
                }).Services
            .AddMediatR(assembly)
            .AddSingleton<ISearchManager, SearchManager>()
            .AddTransient<IVaultService, VaultService>()
            .AddImplementations<IRequestBuilder>(assembly)
            .AddTransient<IVaultClient, VaultClient>();
    }

    private static IServiceCollection AddImplementations<T>(this IServiceCollection services, Assembly assembly)
    {
        assembly
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(T)))
            .Where(type => !type.IsInterface)
            .ToList()
            .ForEach(type => services.AddSingleton(type.GetInterface($"I{type.Name}")!, type));

        return services;
    }
}

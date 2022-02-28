
using FluentVault.Domain.SOAP;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentVault.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddFluentVault(this IServiceCollection services, IConfiguration configuration)
        =>  services
            .Configure<VaultOptions>(configuration.GetSection("Vault"))
            .AddHttpClient("Vault", httpClient =>
                {
                    httpClient.BaseAddress = new Uri($@"http://{configuration.GetSection("Vault:Server").Value}/");
                }).Services
            .AddMediatR(typeof(VaultClient).Assembly)
            .AddSingleton<ISoapRequestService, SoapRequestService>()
            .AddTransient<IVaultClient, VaultClient>();
}

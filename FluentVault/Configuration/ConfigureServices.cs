
using FluentVault.Domain.SOAP;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentVault.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddFluentVault(this IServiceCollection services, IOptions<VaultOptions> options)
        =>  services.AddMediatR(typeof(VaultClient).Assembly)
            .AddSingleton<ISoapRequestService, SoapRequestService>()
            .AddHttpClient("Vault", httpClient =>
                {
                    httpClient.BaseAddress = new Uri($@"http://{options.Value.Server}/");
                }).Services;
}

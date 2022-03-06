using System.Collections.Generic;
using System.Net.Http;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Configuration;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace FluentVault.UnitTests.Systems.Configuration;

public class ServiceCollectionShould
{
    private const string Server = "server";
    private readonly ServiceProvider _provider;

    public ServiceCollectionShould()
    {
        IDictionary<string, string> settings = new Dictionary<string, string>
        {
            ["VaultOptions:Server"] = Server,
            ["VaultOptions:Database"] = "database",
            ["VaultOptions:Username"] = "username",
            ["VaultOptions:Password"] = "password",
            ["VaultOptions:AutoLogin"] = "false",
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        ServiceCollection services = new();
        services.AddFluentVault(configuration);
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public void ReturnSoapRequestService()
        => _provider.GetRequiredService<IVaultRequestService>()
            .Should()
            .NotBeNull();

    [Fact]
    public void ReturnHttpClientFactory()
        => _provider.GetRequiredService<IHttpClientFactory>()
            .Should()
            .NotBeNull();

    [Fact]
    public void ReturnHttpClientFactoryWithBaseAddressFromConfiguration()
        => _provider.GetRequiredService<IHttpClientFactory>()
            .CreateClient("Vault")
            .BaseAddress
            .Should()
            .Be($"http://{Server}");

    [Fact]
    public void ReturnMediator()
        => _provider.GetRequiredService<IMediator>()
            .Should()
            .NotBeNull();

    [Fact]
    public void ReturnVaultClient()
        => _provider.GetRequiredService<IVaultClient>()
            .Should()
            .NotBeNull();
}

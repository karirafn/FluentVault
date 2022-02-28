using System.Collections.Generic;
using System.Net.Http;

using FluentAssertions;

using FluentVault.Configuration;
using FluentVault.Domain.SOAP;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Xunit;

namespace FluentVault.UnitTests.Systems.Configuration;

public class AddFluentVaultShould
{
    private readonly IConfiguration _configuration;
    private readonly ServiceProvider _provider;

    private static readonly ServiceCollection _collection = new();
    private static readonly IDictionary<string, string> _settings = new Dictionary<string, string>
    {
        ["Vault:Server"] = "server",
        ["Vault:Database"] = "database",
        ["Vault:Username"] = "username",
        ["Vault:Password"] = "password",
    };

    public AddFluentVaultShould()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_settings)
            .Build();

        _collection.AddFluentVault(_configuration);
        _provider = _collection.BuildServiceProvider();
    }

    [Fact]
    public void RegisterSoapRequestService()
        => _provider.GetRequiredService<ISoapRequestService>().Should().NotBeNull();

    [Fact]
    public void RegisterHttpClientFactory()
        => _provider.GetRequiredService<IHttpClientFactory>().Should().NotBeNull();

    [Fact]
    public void RegisterMediator()
        => _provider.GetRequiredService<IMediator>().Should().NotBeNull();

    [Fact]
    public void RegisterVaultOptions()
        => _provider.GetRequiredService<IOptions<VaultOptions>>().Should().NotBeNull();

    [Fact]
    public void RegisterVaultOptionsWithDataFromConfiguration()
        => _provider.GetRequiredService<IOptions<VaultOptions>>().Value.Server.Should().Be(_settings["Vault:Server"]);

    [Fact]
    public void RegisterVaultClient()
        => _provider.GetRequiredService<IVaultClient>().Should().NotBeNull();
}

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

public class AddFluentVaultShould
{
    private static readonly ServiceCollection _services = new();
    private static readonly IDictionary<string, string> _settings = new Dictionary<string, string>
    {
        ["Vault:Server"] = "server",
        ["Vault:Database"] = "database",
        ["Vault:Username"] = "username",
        ["Vault:Password"] = "password",
        ["Vault:AutoLogin"] = "false",
    };

    public AddFluentVaultShould()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_settings)
            .Build();

        _services.AddFluentVault(configuration);
    }

    [Fact]
    public void RegisterSoapRequestService()
        => _services.Should().Contain(x => x.ServiceType == typeof(IVaultRequestService));

    [Fact]
    public void RegisterHttpClientFactory()
        => _services.Should().Contain(x => x.ServiceType == typeof(IHttpClientFactory));

    [Fact]
    public void RegisterMediator()
        => _services.Should().Contain(x => x.ServiceType == typeof(IMediator));

    [Fact]
    public void RegisterVaultClient()
        => _services.Should().Contain(x => x.ServiceType == typeof(IVaultClient));
}

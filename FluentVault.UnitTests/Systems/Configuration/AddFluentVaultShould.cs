using System.Net.Http;

using FluentAssertions;

using FluentVault.Common;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace FluentVault.UnitTests.Systems.Configuration;

public class AddFluentVaultShould
{
    private static readonly ServiceCollection _services = new();

    public AddFluentVaultShould()
    {
        _services.AddFluentVault(options =>
        {
            options.Server = "server";
            options.Database = "database";
            options.Password = "password";
            options.Username = "username";
            options.AutoLogin = false;
        });
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

using System;
using System.Net.Http;

using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultHttpClientFactory : IHttpClientFactory
{
    private readonly string _server;

    public VaultHttpClientFactory()
    {
        _server = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptionsFixture>()
            .Build()
            .GetValue<string>(nameof(VaultOptions.Server));
    }

    public HttpClient CreateClient(string name)
        => new() { BaseAddress = new Uri($@"http://{_server}/") };
}

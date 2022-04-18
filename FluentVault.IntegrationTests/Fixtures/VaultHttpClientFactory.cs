using System;
using System.Net.Http;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultHttpClientFactory : IHttpClientFactory
{
    private readonly string _server;

    public VaultHttpClientFactory()
    {
        _server = new VaultOptionsFixture().Create().Value.Server;
    }

    public HttpClient CreateClient(string name)
        => new() { BaseAddress = new Uri($@"http://{_server}/") };
}

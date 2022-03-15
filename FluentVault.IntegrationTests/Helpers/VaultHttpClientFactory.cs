using System;
using System.Net.Http;

namespace FluentVault.IntegrationTests.Helpers;
public class VaultHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
        => new() { BaseAddress = new Uri(@"http://ska-vaultpro/") };
}

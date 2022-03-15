using System.Net.Http;

using FluentVault.Common;

namespace FluentVault.IntegrationTests.Fixtures;
internal class VaultRequestServiceFixture
{
    public VaultRequestServiceFixture()
    {
        
        IHttpClientFactory factory = new VaultHttpClientFactory();
        VaultRequestService = new VaultRequestService(factory);
    }

    public IVaultRequestService VaultRequestService { get; }
}

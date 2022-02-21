using FluentVault.Domain.Common;
using FluentVault.Requests.Get.Categories;
using FluentVault.Requests.Get.Lifecycles;
using FluentVault.Requests.Get.Properties;

namespace FluentVault.Requests.Get;

internal class GetRequest : IGetRequest
{
    private readonly VaultSession _session;

    public GetRequest(VaultSession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<VaultCategory>> Categories() => await new GetCategoriesRequest(_session).SendAsync();
    public async Task<IEnumerable<VaultLifecycle>> Lifecycles() => await new GetLifecyclesRequest(_session).SendAsync();
    public async Task<IEnumerable<VaultPropertyDefinition>> Properties() => await new GetPropertiesRequest(_session).SendAsync();
}


using FluentVault.Requests.Get.Categories;
using FluentVault.Requests.Get.Lifecycles;

namespace FluentVault;

internal class GetRequest : IGetRequest
{
    private readonly VaultSession _session;

    public GetRequest(VaultSession session)
    {
        _session = session;
    }

    public async Task<IEnumerable<VaultCategory>> Categories() => await new GetCategoriesRequest(_session).SendAsync();
    public async Task<IEnumerable<VaultLifecycle>> Lifecycles() => await new GetLifecyclesRequest(_session).SendAsync();
}

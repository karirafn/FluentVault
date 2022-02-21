using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;
using FluentVault.Domain.Common;
using FluentVault.Domain.Lifecycle;

namespace FluentVault.Requests.Get.Lifecycles;

internal class GetLifecyclesRequest : SessionRequest
{
    public GetLifecyclesRequest(VaultSession session)
        : base(session, RequestData.GetAllLifeCycleDefinitions) { }

    public async Task<IEnumerable<VaultLifecycle>> SendAsync()
    {
        StringBuilder innerBody = GenerateInnerBodyFromRequestData();
        XDocument document = await SendRequestAsync(innerBody);
        IEnumerable<VaultLifecycle> lifecycles = document.ParseLifecycles();

        return lifecycles;
    }
}

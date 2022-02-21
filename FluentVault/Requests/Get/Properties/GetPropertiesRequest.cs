using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;
using FluentVault.Domain.Common;
using FluentVault.Domain.Property;

namespace FluentVault.Requests.Get.Properties;

internal class GetPropertiesRequest : SessionRequest
{
    public GetPropertiesRequest(VaultSession session)
        : base(session, RequestData.GetPropertyDefinitionInfosByEntityClassId) { }

    public async Task<IEnumerable<VaultPropertyDefinition>> SendAsync()
    {
        StringBuilder innerBody = GenerateInnerBodyFromRequestData();
        XDocument document = await SendRequestAsync(innerBody);
        IEnumerable<VaultPropertyDefinition> properties = document.ParseProperties();

        return properties;
    }
}

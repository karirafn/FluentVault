using System.Xml.Linq;

namespace FluentVault.Requests.Get.Properties;

internal class GetPropertiesRequest : SessionRequest
{
    public GetPropertiesRequest(VaultSession session) : base(session, "GetPropertyDefinitionInfosByEntityClassId") { }

    public async Task<IEnumerable<VaultProperty>> SendAsync()
    {
        string innerBody = GetOpeningTag(isSelfClosing: true);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultProperty> properties = document.ParseProperties();

        return properties;
    }
}

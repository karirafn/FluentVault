using System.Xml.Linq;

namespace FluentVault;

internal class GetRequest : SessionRequest, IGetRequest
{
    public GetRequest(VaultSession session) : base(session, "GetAllLifeCycleDefinitions") { }

    public async Task<IEnumerable<VaultLifecycle>> Lifecycles()
    {
        string innerBody = GetOpeningTag(isSelfClosing: true);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultLifecycle> lifecycles = document.ParseLifecycles();

        return lifecycles;
    }
}

using System.Xml.Linq;

namespace FluentVault.Requests.Get.Lifecycles;

internal class GetLifecyclesRequest : SessionRequest
{
    public GetLifecyclesRequest(VaultSession session)
        : base(session, RequestData.GetAllLifeCycleDefinitions) { }

    public async Task<IEnumerable<VaultLifecycle>> SendAsync()
    {
        string innerBody = GetOpeningTag(isSelfClosing: true);
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultLifecycle> lifecycles = document.ParseLifecycles();

        return lifecycles;
    }
}

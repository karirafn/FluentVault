using System.Xml.Linq;

namespace FluentVault;

internal abstract class SessionRequest : BaseRequest
{
    protected SessionRequest(VaultSession session, string name) : base(name)
    {
        Session = session;
    }

    public VaultSession Session { get; }

    public async Task<XDocument> SendAsync(string requestBody)
    {
        Uri uri = RequestData.GetRequestUri(RequestName, Session.Server);
        XDocument document = await SendAsync(uri, requestBody);

        return document;
    }
}

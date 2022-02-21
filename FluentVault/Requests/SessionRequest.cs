using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Helpers;

namespace FluentVault.Requests;

internal abstract class SessionRequest : BaseRequest
{
    protected SessionRequest(VaultSession session, RequestData requestData)
        : base(requestData) => Session = session;

    public VaultSession Session { get; }

    public async Task<XDocument> SendRequestAsync(StringBuilder innerBody)
    {
        XDocument document = await SendRequestAsync(innerBody, Session.Server, Session.Ticket, Session.UserId);

        return document;
    }
}

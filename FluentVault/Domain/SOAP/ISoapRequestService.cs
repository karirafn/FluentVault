using System.Xml.Linq;

namespace FluentVault.Domain.SOAP;

internal interface ISoapRequestService
{
    Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder = null);
}

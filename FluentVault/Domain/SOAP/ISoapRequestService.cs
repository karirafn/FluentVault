using System.Xml.Linq;

namespace FluentVault.Domain.SOAP;

internal interface ISoapRequestService
{
    Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session);
    Task<XDocument> SendAsync(string requestName, string requestBody);
    string GetNamespace(string requestName);
}

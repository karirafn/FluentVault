using System.Xml.Linq;

namespace FluentVault.Common;
internal interface IVaultService
{
    Task<XDocument> SendAsync(string uri, string soapAction, XDocument requestBody, CancellationToken cancellationToken = default);
}

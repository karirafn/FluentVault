using System.Xml.Linq;

namespace FluentVault.Common;

internal interface IVaultRequestService
{
    Task<XDocument> SendAsync(string requestName, VaultSessionCredentials session, Action<XElement, XNamespace>? contentBuilder = null);
}

using System.Xml.Linq;

namespace FluentVault.Common;
internal interface IVaultService
{
    Task<XDocument> SendAuthenticatedAsync(
        VaultRequest request,
        Action<XElement, XNamespace>? contentBuilder = null,
        CancellationToken cancellationToken = default);

    Task<XDocument> SendUnauthenticatedAsync(VaultRequest request, Action<XElement, XNamespace>? contentBuilder, CancellationToken cancellationToken = default);
}

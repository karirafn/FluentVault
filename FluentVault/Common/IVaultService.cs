using System.Xml.Linq;

namespace FluentVault.Common;

internal interface IVaultService : IAsyncDisposable
{
    Task<XDocument> SendAsync(VaultRequest request, bool canSignIn, Action<XElement, XNamespace>? contentBuilder = null, CancellationToken cancellationToken = default);
}

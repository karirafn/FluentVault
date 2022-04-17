using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentVault.Common;

using Moq;

namespace FluentVault.UnitTests.Helpers;
internal static class VaultServiceExpressions
{
    public static Expression<Func<IVaultService, Task<XDocument>>>
        SendAsync = x => x.SendAuthenticatedAsync(
            It.IsAny<VaultRequest>(),
            It.IsAny<Action<XElement, XNamespace>?>(),
            It.IsAny<CancellationToken>());
}

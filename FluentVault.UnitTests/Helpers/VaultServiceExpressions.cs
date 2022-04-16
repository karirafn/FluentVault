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
        SendAsync = x => x.SendAsync(
            It.IsAny<string>(),
            It.IsAny<bool>(),
            It.IsAny<Action<XElement, XNamespace>?>(),
            It.IsAny<CancellationToken>());
}

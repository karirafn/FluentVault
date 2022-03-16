using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;
public record VaultInstance(
    VaultInstanceId Id,
    string Name,
    DateTime CreateDate,
    VaultUserId CreateUserId)
{
    internal static VaultInstance Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultInstanceId.Parse),
            element.GetAttributeValue("Name"),
            element.ParseAttributeValue("CreateDate", DateTime.Parse),
            element.ParseAttributeValue("CreateUserId", VaultUserId.Parse));
}

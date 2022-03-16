using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;
public record VaultGroup(
    VaultGroupId Id,
    string Name,
    string Email,
    VaultUserId CreateUserId,
    DateTime CreateDate,
    bool IsActive,
    bool IsSystemGroup,
    VaultAuthenticationType AuthenticationType)
{
    internal static VaultGroup Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultGroupId.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("EmailDL"),
            element.ParseAttributeValue("CreateUserId", VaultUserId.Parse),
            element.ParseAttributeValue("CreateDate", DateTime.Parse),
            element.ParseAttributeValue("IsActive", bool.Parse),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.ParseAttributeValue("Auth", x => VaultAuthenticationType.FromName(x)));
}

using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultUser(
    VaultUserId Id,
    string Name,
    string FirstName,
    string LastName,
    string Email,
    VaultUserId CreateUserId,
    DateTime CreateDate,
    bool IsActive,
    bool IsSystemUser,
    VaultAuthenticationType AuthenticationType)
{
    internal static VaultUser Parse(XElement element)
        => new(element.ParseAttributeValue("Id", VaultUserId.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("FirstName"),
            element.GetAttributeValue("LastName"),
            element.GetAttributeValue("Email"),
            element.ParseAttributeValue("CreateUserId", VaultUserId.Parse),
            element.ParseAttributeValue("CreateDate", DateTime.Parse),
            element.ParseAttributeValue("IsActive", bool.Parse),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.ParseAttributeValue("Auth", x => VaultAuthenticationType.FromName(x)));
}

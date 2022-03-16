using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;
public record VaultRole(
    VaultRoleId Id,
    string Name,
    string SystemName,
    bool IsSystemRole,
    string Description)
{
    internal static VaultRole Parse(XElement element)
        => new(element.ParseAttributeValue("Id", x => VaultRoleId.Parse(x)),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("SysName"),
            element.ParseAttributeValue("IsSys", bool.Parse),
            element.GetAttributeValue("Descr"));
}

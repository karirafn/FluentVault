using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;
public record VaultUserInfo(
    VaultUser User,
    IEnumerable<VaultRole> Roles,
    IEnumerable<VaultInstance> Vaults,
    IEnumerable<VaultGroup> Groups)
{
    internal static IEnumerable<VaultUserInfo> ParseAll(XDocument element)
        => element.ParseAllElements("UserInfo", Parse);

    private static VaultUserInfo Parse(XElement element)
        => new(element.ParseElement("User", VaultUser.Parse),
            element.ParseAllElements("Roles", VaultRole.Parse),
            element.ParseAllElements("Vaults", VaultInstance.Parse),
            element.ParseAllElements("Groups", VaultGroup.Parse));
}

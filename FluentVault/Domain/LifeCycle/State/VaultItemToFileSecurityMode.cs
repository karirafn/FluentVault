using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultItemToFileSecurityMode : SmartEnum<VaultItemToFileSecurityMode>
{
    public static readonly VaultItemToFileSecurityMode ApplyACL = new(nameof(ApplyACL), 1);
    public static readonly VaultItemToFileSecurityMode RemoveACL = new(nameof(RemoveACL), 2);
    public static readonly VaultItemToFileSecurityMode UseItemSecurity = new(nameof(UseItemSecurity), 3);
    public static readonly VaultItemToFileSecurityMode None = new(nameof(None), 4);

    private VaultItemToFileSecurityMode(string name, int value) : base(name, value) { }
}

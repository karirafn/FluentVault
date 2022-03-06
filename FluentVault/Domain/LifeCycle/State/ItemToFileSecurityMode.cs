using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class ItemToFileSecurityMode : SmartEnum<ItemToFileSecurityMode>
{
    public static readonly ItemToFileSecurityMode ApplyACL = new(nameof(ApplyACL), 1);
    public static readonly ItemToFileSecurityMode RemoveACL = new(nameof(RemoveACL), 2);
    public static readonly ItemToFileSecurityMode UseItemSecurity = new(nameof(UseItemSecurity), 3);
    public static readonly ItemToFileSecurityMode None = new(nameof(None), 4);

    private ItemToFileSecurityMode(string name, int value) : base(name, value) { }
}

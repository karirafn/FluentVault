namespace FluentVault;

public class ItemToFileSecurityMode : BaseType
{
    public static readonly ItemToFileSecurityMode ApplyACL = new(nameof(ApplyACL));
    public static readonly ItemToFileSecurityMode RemoveACL = new(nameof(RemoveACL));
    public static readonly ItemToFileSecurityMode UseItemSecurity = new(nameof(UseItemSecurity));
    public static readonly ItemToFileSecurityMode None = new(nameof(None));

    private ItemToFileSecurityMode(string value) : base(value) { }

    public static ItemToFileSecurityMode Parse(string value)
        => Parse(value, new[] { ApplyACL, RemoveACL, UseItemSecurity, None });
}

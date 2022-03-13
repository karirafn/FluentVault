using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultFolderFileSecurityMode : SmartEnum<VaultFolderFileSecurityMode>
{
    public static readonly VaultFolderFileSecurityMode ApplyACL = new(nameof(ApplyACL), 1);
    public static readonly VaultFolderFileSecurityMode RemoveACL = new(nameof(RemoveACL), 2);
    public static readonly VaultFolderFileSecurityMode UseFolderSecurity = new(nameof(UseFolderSecurity), 3);
    public static readonly VaultFolderFileSecurityMode None = new(nameof(None), 4);

    private VaultFolderFileSecurityMode(string name, int value) : base(name, value) { }
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class FolderFileSecurityMode : SmartEnum<FolderFileSecurityMode>
{
    public static readonly FolderFileSecurityMode ApplyACL = new(nameof(ApplyACL), 1);
    public static readonly FolderFileSecurityMode RemoveACL = new(nameof(RemoveACL), 2);
    public static readonly FolderFileSecurityMode UseFolderSecurity = new(nameof(UseFolderSecurity), 3);
    public static readonly FolderFileSecurityMode None = new(nameof(None), 4);

    private FolderFileSecurityMode(string name, int value) : base(name, value) { }
}

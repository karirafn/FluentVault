namespace FluentVault;

public class FolderFileSecurityMode : BaseType
{
    public static readonly FolderFileSecurityMode ApplyACL = new(nameof(ApplyACL));
    public static readonly FolderFileSecurityMode RemoveACL = new(nameof(RemoveACL));
    public static readonly FolderFileSecurityMode UseFolderSecurity = new(nameof(UseFolderSecurity));
    public static readonly FolderFileSecurityMode None = new(nameof(None));

    private FolderFileSecurityMode(string value) : base(value) { }

    public static FolderFileSecurityMode Parse(string value)
        => Parse(value, new[] { ApplyACL, RemoveACL, UseFolderSecurity, None });
}

namespace FluentVault;
public record VaultFolder(
    VaultFolderId Id,
    string Name,
    string Path,
    VaultFolderId ParentFolderId,
    DateTime CreateDate,
    string CreateUserName,
    VaultUserId CreateUserId,
    int ChildFolderCount,
    bool IsLibraryFolder,
    bool IsCloaked,
    bool IsLocked,
    VaultEntityCategory Category);

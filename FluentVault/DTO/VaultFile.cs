namespace FluentVault;

public record VaultFile(
    long Id,
    string Filename,
    long MasterId,
    string VersionName,
    long VersionNumber,
    long MaximumCheckInVersionNumber,
    string Comment,
    DateTime ChecedkInDate,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    long CreateUserId,
    string CreateUserName,
    long CheckSum,
    long FileSize,
    bool IsCheckedOut,
    long FolderId,
    string CheckedOutPath,
    string CheckedOutMachine,
    long CheckedOutUserId,
    string FileClass,
    FileStatus FileStatus,
    bool IsLocked,
    bool IsHidden,
    bool IsCloaked,
    bool IsOnSite,
    bool IsControlledByChangeOrder,
    string DesignVisualAttachmentStatus,
    VaultFileRevision Revision,
    VaultFileLifecycle Lifecycle,
    VaultCategory Category);

public class FileStatus : BaseType
{
    public static readonly FileStatus NeedsUpdating = new(nameof(NeedsUpdating));
    public static readonly FileStatus Unknown = new(nameof(Unknown));
    public static readonly FileStatus UpToDate = new(nameof(UpToDate));

    private FileStatus(string value) : base(value) { }

    public static FileStatus Parse(string value)
        => Parse(value, new[] { NeedsUpdating, Unknown, UpToDate });
}

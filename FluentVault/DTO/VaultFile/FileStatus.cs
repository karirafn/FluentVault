namespace FluentVault;

public class FileStatus : BaseType
{
    public static readonly FileStatus NeedsUpdating = new(nameof(NeedsUpdating));
    public static readonly FileStatus Unknown = new(nameof(Unknown));
    public static readonly FileStatus UpToDate = new(nameof(UpToDate));

    private FileStatus(string value) : base(value) { }

    public static FileStatus Parse(string value)
        => Parse(value, new[] { NeedsUpdating, Unknown, UpToDate });
}

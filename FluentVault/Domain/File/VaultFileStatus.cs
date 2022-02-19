using FluentVault.Common;

namespace FluentVault;

public class VaultFileStatus : BaseType
{
    public static readonly VaultFileStatus NeedsUpdating = new(nameof(NeedsUpdating));
    public static readonly VaultFileStatus Unknown = new(nameof(Unknown));
    public static readonly VaultFileStatus UpToDate = new(nameof(UpToDate));

    private VaultFileStatus(string value) : base(value) { }

    public static VaultFileStatus Parse(string value)
        => Parse(value, new[] { NeedsUpdating, Unknown, UpToDate });
}

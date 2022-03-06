using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultFileStatus : SmartEnum<VaultFileStatus>
{
    public static readonly VaultFileStatus NeedsUpdating = new(nameof(NeedsUpdating), 1);
    public static readonly VaultFileStatus Unknown = new(nameof(Unknown), 2);
    public static readonly VaultFileStatus UpToDate = new(nameof(UpToDate), 3);

    private VaultFileStatus(string name, int value) : base(name, value) { }
}

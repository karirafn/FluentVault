using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultSynchronizePropertiesState : SmartEnum<VaultSynchronizePropertiesState>
{
    public static readonly VaultSynchronizePropertiesState SyncPropAndUpdatePdf = new(nameof(SyncPropAndUpdatePdf), 1);
    public static readonly VaultSynchronizePropertiesState SyncPropAndUpdateView = new(nameof(SyncPropAndUpdateView), 2);
    public static readonly VaultSynchronizePropertiesState SyncPropOnly = new(nameof(SyncPropOnly), 3);
    public static readonly VaultSynchronizePropertiesState None = new(nameof(None), 4);

    private VaultSynchronizePropertiesState(string name, int value) : base(name, value) { }
}

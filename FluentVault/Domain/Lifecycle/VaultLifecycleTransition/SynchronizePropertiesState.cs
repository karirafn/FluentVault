using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class SynchronizePropertiesState : SmartEnum<SynchronizePropertiesState>
{
    public static readonly SynchronizePropertiesState SyncPropAndUpdatePdf = new(nameof(SyncPropAndUpdatePdf), 1);
    public static readonly SynchronizePropertiesState SyncPropAndUpdateView = new(nameof(SyncPropAndUpdateView), 2);
    public static readonly SynchronizePropertiesState SyncPropOnly = new(nameof(SyncPropOnly), 3);
    public static readonly SynchronizePropertiesState None = new(nameof(None), 4);

    private SynchronizePropertiesState(string name, int value) : base(name, value) { }
}

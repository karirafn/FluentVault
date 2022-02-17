namespace FluentVault;

public class SynchronizePropertiesState : BaseType
{
    public static readonly SynchronizePropertiesState SyncPropAndUpdatePdf = new(nameof(SyncPropAndUpdatePdf));
    public static readonly SynchronizePropertiesState SyncPropAndUpdateView = new(nameof(SyncPropAndUpdateView));
    public static readonly SynchronizePropertiesState SyncPropOnly = new(nameof(SyncPropOnly));
    public static readonly SynchronizePropertiesState None = new(nameof(None));

    private SynchronizePropertiesState(string value) : base(value) { }

    public static SynchronizePropertiesState Parse(string value)
        => Parse(value, new[] { SyncPropAndUpdatePdf, SyncPropAndUpdateView, SyncPropOnly, None });
}

namespace FluentVault;

public record VaultLifecycleStateTransition(
    long Id,
    long FromId,
    long ToId,
    BumpRevisionState BumpRevision,
    SynchronizePropertiesState SynchronizeProperties,
    EnforceChildState EnforceChildState,
    EnforceContentState EnforceContentState,
    FileLinkTypeState ItemFileLnkUptodate,
    FileLinkTypeState ItemFileLnkState,
    bool VerifyThatChildIsNotObsolete,
    bool TransitionBasedSecurity,
    bool UpdateItems);

public class BumpRevisionState : BaseType
{
    public static readonly BumpRevisionState BumpProperty = new(nameof(BumpProperty));
    public static readonly BumpRevisionState BumpSecondary = new(nameof(BumpSecondary));
    public static readonly BumpRevisionState BumpTertiary = new(nameof(BumpTertiary));
    public static readonly BumpRevisionState None = new(nameof(None));

    private BumpRevisionState(string value) : base(value) { }

    public static BumpRevisionState Parse(string value)
        => Parse(value, new[] { BumpProperty, BumpSecondary, BumpTertiary, None });
}

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

public class EnforceChildState : BaseType
{
    public static readonly EnforceChildState EnforceChildFiles = new(nameof(EnforceChildFiles));
    public static readonly EnforceChildState EnforceChildFolders = new(nameof(EnforceChildFiles));
    public static readonly EnforceChildState EnforceChildItems = new(nameof(EnforceChildFiles));
    public static readonly EnforceChildState EnforceChildItemsHaveBeenReleased = new(nameof(EnforceChildFiles));
    public static readonly EnforceChildState None = new(nameof(EnforceChildFiles));

    private EnforceChildState(string value) : base(value) { }

    public static EnforceChildState Parse(string value)
        => Parse(value, new[] { EnforceChildFiles, EnforceChildFolders, EnforceChildItems, EnforceChildItemsHaveBeenReleased, None });
}

public class EnforceContentState : BaseType
{
    public static readonly EnforceContentState EnforceFiles = new(nameof(EnforceFiles));
    public static readonly EnforceContentState EnforceLinkToCustomEntities = new(nameof(EnforceLinkToCustomEntities));
    public static readonly EnforceContentState EnforceLinkToFiles = new(nameof(EnforceLinkToFiles));
    public static readonly EnforceContentState EnforceLinkToFolders = new(nameof(EnforceLinkToFolders));
    public static readonly EnforceContentState EnforceLinkToItems = new(nameof(EnforceLinkToItems));
    public static readonly EnforceContentState None = new(nameof(None));

    private EnforceContentState(string value) : base(value) { }

    public static EnforceContentState Parse(string value)
        => Parse(value, new[] { EnforceFiles, EnforceLinkToCustomEntities, EnforceLinkToFiles, EnforceLinkToFolders, EnforceLinkToItems, None });
}

public class FileLinkTypeState : BaseType
{
    public static readonly FileLinkTypeState DesignDocs = new(nameof(DesignDocs));
    public static readonly FileLinkTypeState Primary = new(nameof(Primary));
    public static readonly FileLinkTypeState PrimarySub = new(nameof(PrimarySub));
    public static readonly FileLinkTypeState Secondary = new(nameof(Secondary));
    public static readonly FileLinkTypeState SecondarySub = new(nameof(SecondarySub));
    public static readonly FileLinkTypeState StandardComp = new(nameof(StandardComp));
    public static readonly FileLinkTypeState None = new(nameof(None));

    private FileLinkTypeState(string value) : base(value) { }

    public static FileLinkTypeState Parse(string value)
        => Parse(value, new[] { DesignDocs, Primary, PrimarySub, Secondary, SecondarySub, StandardComp, None });
}

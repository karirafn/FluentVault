using FluentVault.Domain.Common;

namespace FluentVault;

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

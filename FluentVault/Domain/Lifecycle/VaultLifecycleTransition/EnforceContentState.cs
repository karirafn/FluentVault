using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class EnforceContentState : SmartEnum<EnforceContentState>
{
    public static readonly EnforceContentState EnforceFiles = new(nameof(EnforceFiles), 1);
    public static readonly EnforceContentState EnforceLinkToCustomEntities = new(nameof(EnforceLinkToCustomEntities), 2);
    public static readonly EnforceContentState EnforceLinkToFiles = new(nameof(EnforceLinkToFiles), 3);
    public static readonly EnforceContentState EnforceLinkToFolders = new(nameof(EnforceLinkToFolders), 4);
    public static readonly EnforceContentState EnforceLinkToItems = new(nameof(EnforceLinkToItems), 5);
    public static readonly EnforceContentState None = new(nameof(None), 6);

    private EnforceContentState(string name, int value) : base(name, value) { }
}

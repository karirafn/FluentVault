using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultEnforceContentState : SmartEnum<VaultEnforceContentState>
{
    public static readonly VaultEnforceContentState EnforceFiles = new(nameof(EnforceFiles), 1);
    public static readonly VaultEnforceContentState EnforceLinkToCustomEntities = new(nameof(EnforceLinkToCustomEntities), 2);
    public static readonly VaultEnforceContentState EnforceLinkToFiles = new(nameof(EnforceLinkToFiles), 3);
    public static readonly VaultEnforceContentState EnforceLinkToFolders = new(nameof(EnforceLinkToFolders), 4);
    public static readonly VaultEnforceContentState EnforceLinkToItems = new(nameof(EnforceLinkToItems), 5);
    public static readonly VaultEnforceContentState None = new(nameof(None), 6);

    private VaultEnforceContentState(string name, int value) : base(name, value) { }
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultEnforceChildState : SmartEnum<VaultEnforceChildState>
{
    public static readonly VaultEnforceChildState EnforceChildFiles = new(nameof(EnforceChildFiles), 1);
    public static readonly VaultEnforceChildState EnforceChildFolders = new(nameof(EnforceChildFolders), 2);
    public static readonly VaultEnforceChildState EnforceChildItems = new(nameof(EnforceChildItems), 3);
    public static readonly VaultEnforceChildState EnforceChildItemsHaveBeenReleased = new(nameof(EnforceChildItemsHaveBeenReleased), 4);
    public static readonly VaultEnforceChildState None = new(nameof(None), 5);

    private VaultEnforceChildState(string name, int value) : base(name, value) { }
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class EnforceChildState : SmartEnum<EnforceChildState>
{
    public static readonly EnforceChildState EnforceChildFiles = new(nameof(EnforceChildFiles), 1);
    public static readonly EnforceChildState EnforceChildFolders = new(nameof(EnforceChildFolders), 2);
    public static readonly EnforceChildState EnforceChildItems = new(nameof(EnforceChildItems), 3);
    public static readonly EnforceChildState EnforceChildItemsHaveBeenReleased = new(nameof(EnforceChildItemsHaveBeenReleased), 4);
    public static readonly EnforceChildState None = new(nameof(None), 5);

    private EnforceChildState(string name, int value) : base(name, value) { }
}

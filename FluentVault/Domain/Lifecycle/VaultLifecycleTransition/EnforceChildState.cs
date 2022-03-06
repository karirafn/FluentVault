using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class EnforceChildState : SmartEnum<EnforceChildState>
{
    public static readonly EnforceChildState EnforceChildFiles = new(nameof(EnforceChildFiles), 1);
    public static readonly EnforceChildState EnforceChildFolders = new(nameof(EnforceChildFiles), 2);
    public static readonly EnforceChildState EnforceChildItems = new(nameof(EnforceChildFiles), 3);
    public static readonly EnforceChildState EnforceChildItemsHaveBeenReleased = new(nameof(EnforceChildFiles), 4);
    public static readonly EnforceChildState None = new(nameof(EnforceChildFiles), 5);

    private EnforceChildState(string name, int value) : base(name, value) { }
}

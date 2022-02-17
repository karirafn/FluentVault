namespace FluentVault;

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

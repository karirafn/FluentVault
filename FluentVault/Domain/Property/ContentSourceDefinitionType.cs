using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class ContentSourceDefinitionType : SmartEnum<ContentSourceDefinitionType>
{
    public static readonly ContentSourceDefinitionType All = new(nameof(All), 1);
    public static readonly ContentSourceDefinitionType Component = new(nameof(Component), 2);
    public static readonly ContentSourceDefinitionType File = new(nameof(File), 3);
    public static readonly ContentSourceDefinitionType RefDes = new(nameof(RefDes), 4);
    public static readonly ContentSourceDefinitionType None = new(nameof(None), 5);

    private ContentSourceDefinitionType(string name, int value) : base(name, value) { }
}

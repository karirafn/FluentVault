using FluentVault.Domain.Common;

namespace FluentVault;

public class ContentSourceDefinitionType : BaseType
{
    public static readonly ContentSourceDefinitionType All = new(nameof(All));
    public static readonly ContentSourceDefinitionType Component = new(nameof(Component));
    public static readonly ContentSourceDefinitionType File = new(nameof(File));
    public static readonly ContentSourceDefinitionType RefDes = new(nameof(RefDes));
    public static readonly ContentSourceDefinitionType None = new(nameof(None));

    private ContentSourceDefinitionType(string value) : base(value) { }

    public static ContentSourceDefinitionType Parse(string value)
        => Parse(value, new[] { All, Component, File, RefDes, None });
}

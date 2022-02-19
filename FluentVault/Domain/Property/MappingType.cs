using FluentVault.Common;

namespace FluentVault;

public class MappingType : BaseType
{
    public static readonly MappingType Constant = new(nameof(Constant));
    public static readonly MappingType Default = new(nameof(Default));

    private MappingType(string value) : base(value) { }

    public static MappingType Parse(string value)
        => Parse(value, new[] { Constant, Default });
}

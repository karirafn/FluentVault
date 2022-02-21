using FluentVault.Domain.Common;

namespace FluentVault;

public class MappingDirection : BaseType
{
    public static readonly MappingDirection Read = new(nameof(Read));
    public static readonly MappingDirection Write = new(nameof(Write));

    private MappingDirection(string value) : base(value) { }

    public static MappingDirection Parse(string value)
        => Parse(value, new[] { Read, Write });
}

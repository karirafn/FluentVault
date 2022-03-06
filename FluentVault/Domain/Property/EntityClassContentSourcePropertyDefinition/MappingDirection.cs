using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class MappingDirection : SmartEnum<MappingDirection>
{
    public static readonly MappingDirection Read = new(nameof(Read), 1);
    public static readonly MappingDirection Write = new(nameof(Write), 2);

    private MappingDirection(string name, int value) : base(name, value) { }
}

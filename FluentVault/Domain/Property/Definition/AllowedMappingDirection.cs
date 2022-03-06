using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class AllowedMappingDirection : SmartEnum<AllowedMappingDirection>
{
    public static readonly AllowedMappingDirection Read = new(nameof(Read), 1);
    public static readonly AllowedMappingDirection Write = new(nameof(Write), 2);
    public static readonly AllowedMappingDirection ReadAndWrite = new(nameof(ReadAndWrite), 4);
    public static readonly AllowedMappingDirection None = new(nameof(None), 5);

    private AllowedMappingDirection(string name, int value) : base(name, value) { }
}

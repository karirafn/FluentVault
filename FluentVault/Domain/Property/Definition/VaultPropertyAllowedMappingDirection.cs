using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyAllowedMappingDirection : SmartEnum<VaultPropertyAllowedMappingDirection>
{
    public static readonly VaultPropertyAllowedMappingDirection Read = new(nameof(Read), 1);
    public static readonly VaultPropertyAllowedMappingDirection Write = new(nameof(Write), 2);
    public static readonly VaultPropertyAllowedMappingDirection ReadAndWrite = new(nameof(ReadAndWrite), 4);
    public static readonly VaultPropertyAllowedMappingDirection None = new(nameof(None), 5);

    private VaultPropertyAllowedMappingDirection(string name, int value) : base(name, value) { }
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultAllowedMappingDirection : SmartEnum<VaultAllowedMappingDirection>
{
    public static readonly VaultAllowedMappingDirection Read = new(nameof(Read), 1);
    public static readonly VaultAllowedMappingDirection Write = new(nameof(Write), 2);
    public static readonly VaultAllowedMappingDirection ReadAndWrite = new(nameof(ReadAndWrite), 4);
    public static readonly VaultAllowedMappingDirection None = new(nameof(None), 5);

    private VaultAllowedMappingDirection(string name, int value) : base(name, value) { }
}

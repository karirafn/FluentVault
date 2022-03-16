using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyMappingDirection : SmartEnum<VaultPropertyMappingDirection>
{
    public static readonly VaultPropertyMappingDirection Read = new(nameof(Read), 1);
    public static readonly VaultPropertyMappingDirection Write = new(nameof(Write), 2);

    private VaultPropertyMappingDirection(string name, int value) : base(name, value) { }
}

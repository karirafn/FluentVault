using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultMappingDirection : SmartEnum<VaultMappingDirection>
{
    public static readonly VaultMappingDirection Read = new(nameof(Read), 1);
    public static readonly VaultMappingDirection Write = new(nameof(Write), 2);

    private VaultMappingDirection(string name, int value) : base(name, value) { }
}

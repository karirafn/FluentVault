using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultPropertyContentSourceDefinitionType : SmartEnum<VaultPropertyContentSourceDefinitionType>
{
    public static readonly VaultPropertyContentSourceDefinitionType All = new(nameof(All), 1);
    public static readonly VaultPropertyContentSourceDefinitionType Component = new(nameof(Component), 2);
    public static readonly VaultPropertyContentSourceDefinitionType File = new(nameof(File), 3);
    public static readonly VaultPropertyContentSourceDefinitionType RefDes = new(nameof(RefDes), 4);
    public static readonly VaultPropertyContentSourceDefinitionType None = new(nameof(None), 5);

    private VaultPropertyContentSourceDefinitionType(string name, int value) : base(name, value) { }
}

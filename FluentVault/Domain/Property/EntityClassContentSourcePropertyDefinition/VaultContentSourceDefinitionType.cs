using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultContentSourceDefinitionType : SmartEnum<VaultContentSourceDefinitionType>
{
    public static readonly VaultContentSourceDefinitionType All = new(nameof(All), 1);
    public static readonly VaultContentSourceDefinitionType Component = new(nameof(Component), 2);
    public static readonly VaultContentSourceDefinitionType File = new(nameof(File), 3);
    public static readonly VaultContentSourceDefinitionType RefDes = new(nameof(RefDes), 4);
    public static readonly VaultContentSourceDefinitionType None = new(nameof(None), 5);

    private VaultContentSourceDefinitionType(string name, int value) : base(name, value) { }
}

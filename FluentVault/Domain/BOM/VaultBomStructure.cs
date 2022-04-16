
using Ardalis.SmartEnum;

namespace FluentVault;
public sealed class VaultBomStructure : SmartEnum<VaultBomStructure>
{
    public static readonly VaultBomStructure DynamicPhantom = new(nameof(DynamicPhantom), 1);
    public static readonly VaultBomStructure Inseperable = new(nameof(Inseperable), 2);
    public static readonly VaultBomStructure Normal = new(nameof(Normal), 3);
    public static readonly VaultBomStructure Phantom = new(nameof(Phantom), 4);
    public static readonly VaultBomStructure Purchased = new(nameof(Purchased), 5);
    public static readonly VaultBomStructure Reference = new(nameof(Reference), 6);

    private VaultBomStructure(string name, int value) : base(name, value) { }
}


using Ardalis.SmartEnum;

namespace FluentVault;
public class VaultBomStructure : SmartEnum<VaultBomStructure>
{
    private static readonly VaultBomStructure DynamicPhantom = new(nameof(DynamicPhantom), 1);
    private static readonly VaultBomStructure Inseperable = new(nameof(Inseperable), 2);
    private static readonly VaultBomStructure Normal = new(nameof(Normal), 3);
    private static readonly VaultBomStructure Phantom = new(nameof(Phantom), 4);
    private static readonly VaultBomStructure Purchased = new(nameof(Purchased), 5);
    private static readonly VaultBomStructure Reference = new(nameof(Reference), 6);

    private VaultBomStructure(string name, int value) : base(name, value) { }
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultEntityClass : SmartEnum<VaultEntityClass>
{
    private const string FLDR = nameof(FLDR);
    private const string CUSTENT = nameof(CUSTENT);

    public static readonly VaultEntityClass File = new(nameof(File).ToUpper(), 1);
    public static readonly VaultEntityClass Folder = new(FLDR, 2);
    public static readonly VaultEntityClass Item = new(nameof(Item).ToUpper(), 3);
    public static readonly VaultEntityClass CustomEntity = new(CUSTENT, 4);

    private VaultEntityClass(string name, int value) : base(name, value) { }
}

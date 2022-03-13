using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class VaultDataType : SmartEnum<VaultDataType>
{
    public static readonly VaultDataType Bool = new(nameof(Bool), 1);
    public static readonly VaultDataType DateTime = new(nameof(DateTime), 2);
    public static readonly VaultDataType Image = new(nameof(Image), 3);
    public static readonly VaultDataType Numeric = new(nameof(Numeric), 4);
    public static readonly VaultDataType String = new(nameof(String), 5);

    private VaultDataType(string name, int value) : base(name, value) { }
}

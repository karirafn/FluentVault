using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class DataType : SmartEnum<DataType>
{
    public static readonly DataType Bool = new(nameof(Bool), 1);
    public static readonly DataType DateTime = new(nameof(DateTime), 2);
    public static readonly DataType Image = new(nameof(Image), 3);
    public static readonly DataType Numeric = new(nameof(Numeric), 4);
    public static readonly DataType String = new(nameof(String), 5);

    private DataType(string name, int value) : base(name, value) { }
}

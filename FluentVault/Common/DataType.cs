using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class DataType : SmartEnum<DataType>
{
    public static readonly DataType Boolean = new("Bool", 1);
    public static readonly DataType DateTime = new("DateTime", 2);
    public static readonly DataType Image = new("Image", 3);
    public static readonly DataType Numeric = new("Numeric", 4);
    public static readonly DataType Text = new("String", 5);

    private DataType(string name, int value) : base(name, value) { }
}

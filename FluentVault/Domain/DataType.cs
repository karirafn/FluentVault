namespace FluentVault.Domain.Properties;

public class DataType : BaseType
{
    public static readonly DataType Boolean = new("Bool");
    public static readonly DataType DateTime = new("DateTime");
    public static readonly DataType Image = new("Image");
    public static readonly DataType Numeric = new("Numeric");
    public static readonly DataType Text = new("String");

    private DataType(string value) : base(value) { }

    public static DataType Parse(string value)
        => Parse(value, new[] { Boolean, DateTime, Image, Numeric, Text });
}

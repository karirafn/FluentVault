namespace FluentVault.Domain.Common;

public abstract class BaseType
{
    public string Value { get; init; }

    public BaseType(string value)
    {
        Value = value;
    }

    protected static T Parse<T>(string value, IEnumerable<T> types) where T : BaseType
        => types.FirstOrDefault(type => type.Value.Equals(value))
        ?? throw new FormatException($@"Unable to parse value ""{value}"" to type ""{typeof(T)}""");

    public override string ToString() => Value;
}

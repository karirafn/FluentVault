namespace FluentVault;

public abstract class BaseType
{
    public string Value { get; init; }

    public BaseType(string value)
    {
        Value = value;
    }

    protected static T Parse<T>(string value, IEnumerable<T> types) where T : BaseType
        => types.FirstOrDefault(x => x.Value.Equals(value))
        ?? throw new ArgumentException($@"Unable to parse ""{value}"" to type ""{typeof(T)}""", nameof(value));
}

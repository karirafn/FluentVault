namespace FluentVault;

public sealed class SearchPropertyType
{
    public static readonly SearchPropertyType SingleProperty = new(nameof(SingleProperty));
    public static readonly SearchPropertyType AllProperties = new(nameof(AllProperties));
    public static readonly SearchPropertyType AllPropertiesAndContent = new(nameof(AllPropertiesAndContent));

    private readonly string _value;

    private SearchPropertyType(string value) => _value = value;

    public override string ToString() => _value;
}

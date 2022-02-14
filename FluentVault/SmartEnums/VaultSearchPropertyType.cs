namespace FluentVault;

public sealed class VaultSearchPropertyType
{
    public static readonly VaultSearchPropertyType SingleProperty = new(nameof(SingleProperty));
    public static readonly VaultSearchPropertyType AllProperties = new(nameof(AllProperties));
    public static readonly VaultSearchPropertyType AllPropertiesAndContent = new(nameof(AllPropertiesAndContent));

    private readonly string _value;

    private VaultSearchPropertyType(string value) => _value = value;

    public override string ToString() => _value;
}

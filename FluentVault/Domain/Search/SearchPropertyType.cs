using Ardalis.SmartEnum;

namespace FluentVault.Domain.Search;

internal sealed class SearchPropertyType : SmartEnum<SearchPropertyType>
{
    public static readonly SearchPropertyType SingleProperty = new(nameof(SingleProperty), 1);
    public static readonly SearchPropertyType AllProperties = new(nameof(AllProperties), 2);
    public static readonly SearchPropertyType AllPropertiesAndContent = new(nameof(AllPropertiesAndContent), 3);

    private SearchPropertyType(string name, int value) : base(name, value) { }
}

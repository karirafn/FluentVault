using System.Globalization;

namespace FluentVault.Domain.Search;

internal class SearchCondition
{
    private readonly VaultPropertyDefinitionId _propertyId;
    private readonly SearchOperator _searchOperator;
    private readonly string _searchText;
    private readonly SearchPropertyType _propertyType;
    private readonly SearchRule _searchRule;

    public SearchCondition(VaultPropertyDefinitionId propertyId, SearchOperator searchOperator, object searchValue, SearchPropertyType propertyType, SearchRule serachRule)
    {
        (_propertyId, _searchOperator, _propertyType, _searchRule) = (propertyId, searchOperator, propertyType, serachRule);

        _searchText = searchValue switch
        {
            string s => s,
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            not null => searchValue.ToString() ?? throw new Exception("Unable to convert search value to string"),
            _ => string.Empty
        };
    }

    public IDictionary<string, object> Attributes => new Dictionary<string, object>
    {
        ["PropDefId"] = _propertyId,
        ["SrchOper"] = _searchOperator.Value,
        ["SrchTxt"] = _searchText,
        ["PropTyp"] = _propertyType.Name,
        ["SrchRule"] = _searchRule,
    };
}

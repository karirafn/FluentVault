namespace FluentVault.Domain.Search;

internal class SortCondition
{
    private readonly long _propertyId;
    private readonly bool _sortAscending;

    public SortCondition(long propertyId, bool sortAscending)
        => (_propertyId, _sortAscending) = (propertyId, sortAscending);

    public IDictionary<string, string> Attributes => new Dictionary<string, string>
    {
        ["PropDefId"] = _propertyId.ToString(),
        ["SortAsc"] = _sortAscending.ToString().ToLower(),
    };
}

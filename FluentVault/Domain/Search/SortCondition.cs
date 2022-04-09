namespace FluentVault.Domain.Search;

internal class SortCondition
{
    private readonly long _propertyId;
    private readonly bool _sortAscending;

    public SortCondition(long propertyId, bool sortAscending)
        => (_propertyId, _sortAscending) = (propertyId, sortAscending);

    public IDictionary<string, object> Attributes => new Dictionary<string, object>
    {
        ["PropDefId"] = _propertyId,
        ["SortAsc"] = _sortAscending,
    };
}

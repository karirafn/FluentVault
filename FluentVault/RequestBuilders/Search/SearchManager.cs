using System.Globalization;

using FluentVault.Domain.Search;

namespace FluentVault.RequestBuilders.Search;
internal class SearchManager : ISearchManager
{
    private readonly List<SearchCondition> _searchConditions = new();
    private readonly List<SortCondition> _sortConditions = new();
    private readonly List<VaultFolderId> _folderIds = new();

    public object? SearchValue { get; set; }
    public SearchPropertyType PropertyType { get; set; } = SearchPropertyType.SingleProperty;
    public VaultPropertyDefinitionId? SearchConditionPropertyId { get; set; }
    public VaultPropertyDefinitionId? SortConditionPropertyId { get; set; }
    public SearchOperator SearchOperator { get; set; } = SearchOperator.Contains;
    public bool RecurseFolders { get; set; } = true;
    public bool LatestOnly { get; set; } = true;
    public IEnumerable<SearchCondition> SearchConditions => _searchConditions;
    public IEnumerable<SortCondition> SortConditions => _sortConditions;
    public IEnumerable<VaultFolderId> FolderIds => _folderIds;

    public void AddSearchCondition()
    {
        if (SearchConditionPropertyId is null)
            return;

        string searchText = SearchValue switch
        {
            string s => s,
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            not null => SearchValue.ToString() ?? throw new Exception("Unable to convert search value to string"),
            _ => string.Empty
        };

        SearchCondition searchCondition = new(SearchConditionPropertyId, SearchOperator, searchText, PropertyType, SearchRule.Must);
        _searchConditions.Add(searchCondition);
        PropertyType = SearchPropertyType.SingleProperty;
    }

    public void AddSortCondition()
    {
        if (SortConditionPropertyId is null)
            return;

        SortCondition sortCondition = new(SortConditionPropertyId, true);
        _sortConditions.Add(sortCondition);
    }

    public void Reset()
    {
        _searchConditions.Clear();
        _sortConditions.Clear();
        _folderIds.Clear();
        SearchValue = null;
        RecurseFolders = true;
        LatestOnly = true;
    }

    public void AddFolderId(VaultFolderId id) => _folderIds.Add(id);
    public void AddFolderIds(IEnumerable<VaultFolderId> ids) => _folderIds.AddRange(ids);
}

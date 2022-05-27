
using FluentVault.Domain.Search;

namespace FluentVault.RequestBuilders.Search;

internal interface ISearchManager
{
    IEnumerable<VaultFolderId> FolderIds { get; }
    IEnumerable<SearchCondition> SearchConditions { get; }
    IEnumerable<SortCondition> SortConditions { get; }
    object? SearchValue { get; set; }
    SearchPropertyType PropertyType { get; set; }
    VaultPropertyDefinitionId? SearchConditionPropertyId { get; set; }
    VaultPropertyDefinitionId? SortConditionPropertyId { get; set; }
    SearchOperator SearchOperator { get; set; }
    bool LatestOnly { get; set; }
    bool RecurseFolders { get; set; }
    void AddFolderId(VaultFolderId id);
    void AddFolderIds(IEnumerable<VaultFolderId> ids);
    void AddSearchCondition();
    void AddSortCondition();
    void Reset();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentVault.Domain.Search;
using FluentVault.Domain.Search.Items;
using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Search.Items;
internal class SearchItemsRequestBuilder :
    IRequestBuilder,
    ISearchItemsRequestBuilder,
    ISearchItemsOperatorSelector,
    ISearchItemsEndpoint
{
    private readonly IMediator _mediator;
    private readonly ISearchManager _searchManager;

    public SearchItemsRequestBuilder(IMediator mediator, ISearchManager searchManager)
    {
        _mediator = mediator;
        _searchManager = searchManager;
    }

    public ISearchItemsOperatorSelector ByAllProperties
    {
        get
        {
            _searchManager.PropertyType = SearchPropertyType.AllProperties;
            return this;
        }
    }

    public ISearchItemsOperatorSelector ByAllPropertiesAndContent
    {
        get
        {
            _searchManager.PropertyType = SearchPropertyType.AllPropertiesAndContent;
            return this;
        }
    }

    public ISearchItemsRequestBuilder And
    {
        get
        {
            _searchManager.AddSearchCondition();
            return this;
        }
    }

    public ISearchItemsEndpoint AllVersions
    {
        get
        {
            _searchManager.LatestOnly = false;
            return this;
        }
    }

    public ISearchItemsOperatorSelector ByPropertyId(VaultPropertyDefinitionId id)
    {
        _searchManager.SearchConditionPropertyId = id;
        _searchManager.PropertyType = SearchPropertyType.SingleProperty;
        return this;
    }

    public ISearchItemsOperatorSelector BySystemProperty(VaultSearchProperty property)
        => ByPropertyId(property.Value);

    public ISearchItemsEndpoint Containing(object? value) => SetValueAndOperator(value, SearchOperator.Contains);
    public ISearchItemsEndpoint EqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsEqualTo);
    public ISearchItemsEndpoint NotEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsNotEqualTo);
    public ISearchItemsEndpoint LessThan(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThan);
    public ISearchItemsEndpoint LessThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchItemsEndpoint GreaterThan(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThan);
    public ISearchItemsEndpoint GreaterThanOrEqualTo(object? value) => SetValueAndOperator(value, SearchOperator.IsGreaterThanOrEqualTo);

    public async Task<IEnumerable<VaultItem>> GetAllResultsAsync()
    {
        IEnumerable<VaultItem> items = await GetPagedResultAsync(int.MaxValue);
        return items;
    }

    public async Task<IEnumerable<VaultItem>> GetPagedResultAsync(int pagingLimit = 100)
    {
        IEnumerable<VaultItem> items = await SearchAsync(pagingLimit);
        return items;
    }

    public async Task<VaultItem?> GetFirstResultAsync()
    {
        IEnumerable<VaultItem> items = await GetPagedResultAsync();
        return items.FirstOrDefault();
    }

    private async Task<IEnumerable<VaultItem>> SearchAsync(int pagingLimit)
    {
        _searchManager.AddSearchCondition();

        List<VaultItem> items = new();
        string bookmark = string.Empty;
        do
        {
            FindItemRevisionsBySearchConditionsQuery query = new(
                _searchManager.SearchConditions,
                _searchManager.SortConditions,
                _searchManager.FolderIds,
                _searchManager.RecurseFolders,
                _searchManager.LatestOnly,
                bookmark);
            VaultSearchItemsResponse response = await _mediator.Send(query);
            items.AddRange(response.Result.Items);
            bookmark = response.Bookmark;
        } while (items.Count <= pagingLimit && string.IsNullOrEmpty(bookmark) is false);
        return items;
    }

    private ISearchItemsEndpoint SetValueAndOperator(object? value, SearchOperator @operator)
    {
        _searchManager.SearchValue = value;
        _searchManager.SearchOperator = @operator;
        return this;
    }
}

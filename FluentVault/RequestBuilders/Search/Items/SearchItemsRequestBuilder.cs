using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentVault.Domain.Search;

using MediatR;

namespace FluentVault.RequestBuilders.Search.Items;
//internal class SearchItemsRequestBuilder :
//    IRequestBuilder,
//    ISearchItemsRequestBuilder,
//    ISearchItemsOperatorSelector,
//    ISearchItemsEndpoint
//{
//    private readonly IMediator _mediator;

//    private object? _value = new();
//    private VaultPropertyDefinitionId? _propertyId;
//    private string _propertyName = string.Empty;
//    private SearchPropertyType _propertyType = SearchPropertyType.SingleProperty;
//    private SearchOperator _operator = SearchOperator.Contains;
//    private IEnumerable<VaultProperty> _allProperties = new List<VaultProperty>();
//    private bool _latestOnly = true;
//    private readonly List<SearchCondition> _searchConditions = new();
//    private readonly List<SortCondition> _sortConditions = new();

//    public SearchItemsRequestBuilder(IMediator mediator)
//    {
//        _mediator = mediator;
//    }
//}

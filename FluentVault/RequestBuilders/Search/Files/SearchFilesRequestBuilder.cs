﻿using System.Globalization;

using FluentVault.Domain;
using FluentVault.Domain.Search;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Requests.Search.Files;

internal class SearchFilesRequestBuilder :
    ISearchFilesRequestBuilder,
    ISearchFilesBooleanProperty,
    ISearchFilesDateTimeProperty,
    ISearchFilesNumericProperty,
    ISearchFilesStringProperty,
    ISearchFilesAddSearchCondition
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    private object _searchConditionValue = new();
    private long _searchConditionPropertyId;
    private long _searchConditionOperator;
    private string _propertyName = string.Empty;
    private SearchPropertyType _searchConditionPropertyType = SearchPropertyType.SingleProperty;
    private IEnumerable<VaultPropertyDefinition> _allProperties = new List<VaultPropertyDefinition>();
    private bool _recurseFolders = true;
    private bool _latestOnly = true;
    private readonly List<long> _folderIds = new();
    private readonly List<IDictionary<string, string>> _searchConditions = new();
    private readonly List<IDictionary<string, string>> _sortConditions = new();

    public SearchFilesRequestBuilder(IMediator mediator, VaultSessionCredentials session)
    {
        _mediator = mediator;
        _session = session;
    }

    public async Task<IEnumerable<VaultFile>> SearchWithoutPaging()
    {
        IEnumerable<VaultFile> files = await SearchAsync(int.MaxValue);
        return files;
    }

    public async Task<IEnumerable<VaultFile>> SearchWithPaging(int maxResultCount = 200)
    {
        IEnumerable<VaultFile> files = await SearchAsync(maxResultCount);
        return files;
    }

    public async Task<VaultFile?> SearchSingleAsync()
    {
        IEnumerable<VaultFile> files = await SearchWithPaging();
        return files.FirstOrDefault();
    }

    public ISearchFilesBooleanProperty ForValueEqualTo(bool value) => SetBooleanValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesBooleanProperty ForValueNotEqualTo(bool value) => SetBooleanValue(value, SearchOperator.IsNotEqualTo);

    public ISearchFilesDateTimeProperty ForValueEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesDateTimeProperty ForValueNotEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesDateTimeProperty ForValueLessThan(DateTime value) => SetDateTimeValue(value, SearchOperator.IsLessThan);
    public ISearchFilesDateTimeProperty ForValueLessThanOrEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesDateTimeProperty ForValueGreaterThan(DateTime value) => SetDateTimeValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesDateTimeProperty ForValueGreaterThanOrEqualTo(DateTime value) => SetDateTimeValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesNumericProperty ForValueEqualTo(int value) => SetNumericValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesNumericProperty ForValueNotEqualTo(int value) => SetNumericValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesNumericProperty ForValueLessThan(int value) => SetNumericValue(value, SearchOperator.IsLessThan);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(int value) => SetNumericValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesNumericProperty ForValueGreaterThan(int value) => SetNumericValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(int value) => SetNumericValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesNumericProperty ForValueEqualTo(double value) => SetNumericValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesNumericProperty ForValueNotEqualTo(double value) => SetNumericValue(value, SearchOperator.IsNotEqualTo);
    public ISearchFilesNumericProperty ForValueLessThan(double value) => SetNumericValue(value, SearchOperator.IsLessThan);
    public ISearchFilesNumericProperty ForValueLessThanOrEqualTo(double value) => SetNumericValue(value, SearchOperator.IsLessThanOrEqualTo);
    public ISearchFilesNumericProperty ForValueGreaterThan(double value) => SetNumericValue(value, SearchOperator.IsGreaterThan);
    public ISearchFilesNumericProperty ForValueGreaterThanOrEqualTo(double value) => SetNumericValue(value, SearchOperator.IsGreaterThanOrEqualTo);

    public ISearchFilesStringProperty ForValueEqualTo(string value) => SetStringValue(value, SearchOperator.IsEqualTo);
    public ISearchFilesStringProperty ForValueContaining(string value) => SetStringValue(value, SearchOperator.Contains);
    public ISearchFilesStringProperty ForValueNotContaining(string value) => SetStringValue(value, SearchOperator.DoesNotContain);

    public ISearchFilesAddSearchCondition InUserProperty(string name)
    {
        _propertyName = name;
        return this;
    }

    public ISearchFilesAddSearchCondition InSystemProperty(SearchBooleanProperties property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchDateTimeProperty property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchNumericProperty property) => SetProperty((long)property);
    public ISearchFilesAddSearchCondition InSystemProperty(SearchStringProperty property) => SetProperty((long)property);

    public ISearchFilesAddSearchCondition InAllProperties
    {
        get
        {
            _searchConditionPropertyType = SearchPropertyType.AllProperties;
            return this;
        }
    }

    public ISearchFilesAddSearchCondition InAllPropertiesAndContent
    {
        get
        {
            _searchConditionPropertyType = SearchPropertyType.AllPropertiesAndContent;
            return this;
        }
    }

    public ISearchFilesRequestBuilder And
    {
        get
        {
            AddSearchCondition();
            return this;
        }
    }

    private async Task SetPropertyValue(string property)
    {
        if (_allProperties.Any() is false)
            _allProperties = await _mediator.Send(new GetPropertyDefinitionsQuery(_session));

        var selectedProperty = _allProperties.FirstOrDefault(x => x.Definition.DisplayName.Equals(property))
            ?? throw new KeyNotFoundException($@"Property ""{property}"" was not found");
        _searchConditionPropertyId = selectedProperty.Definition.Id;
    }

    private async Task<IEnumerable<VaultFile>> SearchAsync(int maxResultCount)
    {
        if (string.IsNullOrEmpty(_propertyName) is false)
            await SetPropertyValue(_propertyName);

        AddSearchCondition();

        List<VaultFile> files = new();
        string bookmark = string.Empty;
        do
        {
            var command = new SearchFilesCommand(_searchConditions, _sortConditions, _folderIds, _recurseFolders, _latestOnly, bookmark, _session);
            var result = await _mediator.Send(command);
            files.AddRange(result.Files);
            bookmark = result.Bookmark;
        } while (files.Count <= maxResultCount && string.IsNullOrEmpty(bookmark) is false);

        return files;
    }

    private void AddSearchCondition()
    {
        string searchConditionValue = _searchConditionValue switch
        {
            string s => s,
            DateTime d => d.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            not null => _searchConditionValue.ToString() ?? throw new Exception("Unable to convert search value to string"),
            _ => string.Empty
        };

        var searchCondition = GetSearchCondition(_searchConditionPropertyId, _searchConditionOperator, searchConditionValue, _searchConditionPropertyType, SearchRule.Must);
        _searchConditions.Add(searchCondition);
        _searchConditionPropertyType = SearchPropertyType.SingleProperty;
    }

    private static IDictionary<string, string> GetSearchCondition(long propertyId, long searchOperator, string searchText, SearchPropertyType type, SearchRule rule)
        => new Dictionary<string, string>
            {
                ["PropDefId"] = propertyId.ToString(),
                ["SrchOper"] = searchOperator.ToString(),
                ["SrchTxt"] = searchText,
                ["PropTyp"] = type.ToString(),
                ["SrchRule"] = rule.ToString(),
            };

    private static IDictionary<string, string> GetSortCondition(long propertyId, bool sortAscending)
        => new Dictionary<string, string>
            {
                ["PropDefId"] = propertyId.ToString(),
                ["SortAsc"] = sortAscending.ToString().ToLower(),
            };

    private ISearchFilesBooleanProperty SetBooleanValue(bool value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesDateTimeProperty SetDateTimeValue(DateTime value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesNumericProperty SetNumericValue(int value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesNumericProperty SetNumericValue(double value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private ISearchFilesStringProperty SetStringValue(string value, SearchOperator @operator)
    {
        SetValue(value, @operator);
        return this;
    }

    private void SetValue(object value, SearchOperator @operator)
    {
        _searchConditionValue = value;
        _searchConditionOperator = (long)@operator;
    }

    private ISearchFilesAddSearchCondition SetProperty(long value)
    {
        _searchConditionPropertyId = value;
        _searchConditionPropertyType = SearchPropertyType.SingleProperty;
        return this;
    }
}
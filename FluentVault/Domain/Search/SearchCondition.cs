namespace FluentVault.Domain.Search;
internal record SearchCondition(VaultPropertyDefinitionId PropertyId, SearchOperator SearchOperator, string SearchText, SearchPropertyType PropertyType, SearchRule SearchRule);

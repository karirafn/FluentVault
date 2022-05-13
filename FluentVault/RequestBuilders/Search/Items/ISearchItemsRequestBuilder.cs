namespace FluentVault;
public interface ISearchItemsRequestBuilder
{
    ISearchItemsOperatorSelector ByPropertyId(VaultPropertyDefinitionId id);
    ISearchItemsOperatorSelector BySystemProperty(VaultSearchProperty property);
    ISearchItemsOperatorSelector ByAllProperties { get; }
    ISearchItemsOperatorSelector ByAllPropertiesAndContent { get; }
}

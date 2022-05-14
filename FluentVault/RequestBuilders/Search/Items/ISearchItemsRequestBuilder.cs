namespace FluentVault;
public interface ISearchItemsRequestBuilder
{
    ISearchItemsOperatorSelector ByPropertyId(VaultPropertyDefinitionId id);
    ISearchItemsOperatorSelector BySystemProperty(VaultSystemProperty property);
    ISearchItemsOperatorSelector ByAllProperties { get; }
    ISearchItemsOperatorSelector ByAllPropertiesAndContent { get; }
}

namespace FluentVault;
public interface ISearchItemsRequestBuilder
{
    ISearchItemsOperatorSelector ByUserProperty(string name);
    ISearchItemsOperatorSelector BySystemProperty(VaultSearchProperty property);
    ISearchItemsOperatorSelector ByAllProperties { get; }
    ISearchItemsOperatorSelector ByAllPropertiesAndContent { get; }
}

namespace FluentVault;
public interface ISearchFilesRequestBuilder
{
    ISearchFilesOperatorSelector ByUserProperty(string name);
    ISearchFilesOperatorSelector BySystemProperty(VaultSearchProperty property);
    ISearchFilesOperatorSelector ByAllProperties { get; }
    ISearchFilesOperatorSelector ByAllPropertiesAndContent { get; }
}

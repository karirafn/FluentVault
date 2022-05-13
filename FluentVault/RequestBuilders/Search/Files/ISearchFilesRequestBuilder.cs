namespace FluentVault;
public interface ISearchFilesRequestBuilder
{
    ISearchFilesOperatorSelector ByPropertyId(VaultPropertyDefinitionId id);
    ISearchFilesOperatorSelector BySystemProperty(VaultSearchProperty property);
    ISearchFilesOperatorSelector ByAllProperties { get; }
    ISearchFilesOperatorSelector ByAllPropertiesAndContent { get; }
}

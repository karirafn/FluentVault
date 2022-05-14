namespace FluentVault;
public interface ISearchFilesRequestBuilder
{
    ISearchFilesOperatorSelector ByPropertyId(VaultPropertyDefinitionId id);
    ISearchFilesOperatorSelector BySystemProperty(VaultSystemProperty property);
    ISearchFilesOperatorSelector ByAllProperties { get; }
    ISearchFilesOperatorSelector ByAllPropertiesAndContent { get; }
}

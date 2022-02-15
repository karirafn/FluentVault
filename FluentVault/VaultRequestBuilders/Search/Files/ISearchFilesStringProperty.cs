namespace FluentVault;

public interface ISearchFilesStringProperty
{
    public IAddSearchFilesCondition InProperty(SearchStringProperty property);
    public IAddSearchFilesCondition InAllProperties { get; }
    public IAddSearchFilesCondition InAllPropertiesAndContent { get; }
}

namespace FluentVault;

public interface ISearchFilesOperatorSelector
{
    ISearchFilesEndpoint Containing(object? value);
    ISearchFilesEndpoint EqualTo(object? value);
    ISearchFilesEndpoint NotEqualTo(object? value);
    ISearchFilesEndpoint LessThan(object? value);
    ISearchFilesEndpoint LessThanOrEqualTo(object? value);
    ISearchFilesEndpoint GreaterThan(object? value);
    ISearchFilesEndpoint GreaterThanOrEqualTo(object? value);
}

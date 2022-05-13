namespace FluentVault;
public interface ISearchItemsOperatorSelector
{
    ISearchItemsEndpoint Containing(object? value);
    ISearchItemsEndpoint EqualTo(object? value);
    ISearchItemsEndpoint NotEqualTo(object? value);
    ISearchItemsEndpoint LessThan(object? value);
    ISearchItemsEndpoint LessThanOrEqualTo(object? value);
    ISearchItemsEndpoint GreaterThan(object? value);
    ISearchItemsEndpoint GreaterThanOrEqualTo(object? value);
}

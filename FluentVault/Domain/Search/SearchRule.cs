namespace FluentVault.Domain.Search;

internal class SearchRule
{
    public static readonly SearchRule Must = new(nameof(Must));
    public static readonly SearchRule May = new(nameof(May));

    private readonly string _value;

    private SearchRule(string value) => _value = value;

    public override string ToString() => _value;
}

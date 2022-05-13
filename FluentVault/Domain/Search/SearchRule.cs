using Ardalis.SmartEnum;

namespace FluentVault.Domain.Search;

internal class SearchRule : SmartEnum<SearchRule>
{
    public static readonly SearchRule Must = new(nameof(Must), 1);
    public static readonly SearchRule May = new(nameof(May), 2);

    public SearchRule(string name, int value) : base(name, value)
    {
    }
}

using Ardalis.SmartEnum;

namespace FluentVault.Domain.Search;

internal sealed class SearchOperator : SmartEnum<SearchOperator>
{
    public static readonly SearchOperator Contains = new(nameof(Contains), 1);
    public static readonly SearchOperator DoesNotContain = new(nameof(DoesNotContain), 2);
    public static readonly SearchOperator IsEqualTo = new(nameof(IsEqualTo), 3);
    public static readonly SearchOperator IsEmpty = new(nameof(IsEmpty), 4);
    public static readonly SearchOperator IsNotEmpty = new(nameof(IsNotEmpty), 5);
    public static readonly SearchOperator IsGreaterThan = new(nameof(IsGreaterThan), 5);
    public static readonly SearchOperator IsGreaterThanOrEqualTo = new(nameof(IsGreaterThanOrEqualTo), 7);
    public static readonly SearchOperator IsLessThan = new(nameof(IsLessThan), 8);
    public static readonly SearchOperator IsLessThanOrEqualTo = new(nameof(IsLessThanOrEqualTo), 9);
    public static readonly SearchOperator IsNotEqualTo = new(nameof(IsNotEqualTo), 10);

    public SearchOperator(string name, int value) : base(name, value) { }
}

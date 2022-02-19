namespace FluentVault.Domain.Search;

internal enum SearchOperator
{
    Contains = 1,
    DoesNotContain = 2,
    IsEqualTo = 3,
    IsEmpty = 4,
    IsNotEmpty = 5,
    IsGreaterThan = 6,
    IsGreaterThanOrEqualTo = 7,
    IsLessThan = 8,
    IsLessThanOrEqualTo = 9,
    IsNotEqualTo = 10,
}

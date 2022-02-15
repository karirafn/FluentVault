namespace FluentVault;

public class SearchOperator
{
    public static readonly SearchOperator Contains = new(1);
    public static readonly SearchOperator DoesNotContain = new(2);
    public static readonly SearchOperator IsEqualTo = new(3);
    public static readonly SearchOperator IsEmpty = new(4);
    public static readonly SearchOperator IsNotEmpty = new(5);
    public static readonly SearchOperator IsGreaterThan = new(6);
    public static readonly SearchOperator IsGreaterThanOrEqualTo = new(7);
    public static readonly SearchOperator IsLessThan = new(8);
    public static readonly SearchOperator IsLessThanOrEqualTo = new(9);
    public static readonly SearchOperator IsNotEqualTo = new(10);

    private readonly int _value;

    private SearchOperator(int value) => _value = value;

    public override string ToString() => _value.ToString();
}

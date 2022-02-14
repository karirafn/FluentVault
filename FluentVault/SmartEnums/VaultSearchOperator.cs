namespace FluentVault;

public class VaultSearchOperator
{
    public static readonly VaultSearchOperator Contains = new(1);
    public static readonly VaultSearchOperator DoesNotContain = new(2);
    public static readonly VaultSearchOperator IsEqualTo = new(3);
    public static readonly VaultSearchOperator IsEmpty = new(4);
    public static readonly VaultSearchOperator IsNotEmpty = new(5);
    public static readonly VaultSearchOperator IsGreaterThan = new(6);
    public static readonly VaultSearchOperator IsGreaterThanOrEqualTo = new(7);
    public static readonly VaultSearchOperator IsLessThan = new(8);
    public static readonly VaultSearchOperator IsLessThanOrEqualTo = new(9);
    public static readonly VaultSearchOperator IsNotEqualTo = new(10);

    private readonly int _value;

    private VaultSearchOperator(int value) => _value = value;

    public override string ToString() => _value.ToString();
}

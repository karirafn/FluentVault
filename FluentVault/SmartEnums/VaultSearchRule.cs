namespace FluentVault;

public class VaultSearchRule
{
    public static readonly VaultSearchRule Must = new(nameof(Must));
    public static readonly VaultSearchRule May = new(nameof(May));

    private readonly string _value;

    private VaultSearchRule(string value) => _value = value;

    public override string ToString() => _value;
}

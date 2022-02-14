namespace FluentVault;

public class VaultSearchProperty
{
    public static readonly VaultSearchProperty Filename = new(9);
    public static readonly VaultSearchProperty LifecycleState = new(49);

    private readonly int _value;

    private VaultSearchProperty(int value) => _value = value;

    public override string ToString() => _value.ToString();
}

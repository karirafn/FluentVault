namespace FluentVault;

public class VaultProperty
{
    public static readonly VaultProperty Filename = new(9);
    public static readonly VaultProperty LifecycleState = new(49);

    private readonly int _value;

    private VaultProperty(int value) => _value = value;

    public override string ToString() => _value.ToString();
}

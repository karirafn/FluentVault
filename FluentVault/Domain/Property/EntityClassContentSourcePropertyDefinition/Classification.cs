using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class Classification : SmartEnum<Classification>
{
    public static readonly Classification Standard = new(nameof(Standard), 1);
    public static readonly Classification Custom = new(nameof(Custom), 2);
    public static readonly Classification None = new(nameof(None), 3);

    private Classification(string name, int value) : base(name, value) { }
}

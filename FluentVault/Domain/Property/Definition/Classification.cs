using FluentVault.Common;

namespace FluentVault;

public class Classification : BaseType
{
    public static readonly Classification Standard = new(nameof(Standard));
    public static readonly Classification Custom = new(nameof(Custom));
    public static readonly Classification None = new(nameof(None));

    private Classification(string value) : base(value) { }

    public static Classification Parse(string value)
        => Parse(value, new[] { Standard, Custom, None });
}

using Ardalis.SmartEnum;

namespace FluentVault;

public sealed class RestrictPurgeOption : SmartEnum<RestrictPurgeOption>
{
    public static readonly RestrictPurgeOption All = new(nameof(All), 1);
    public static readonly RestrictPurgeOption FirstAndLast = new(nameof(FirstAndLast), 2);
    public static readonly RestrictPurgeOption Last = new(nameof(Last), 3);
    public static readonly RestrictPurgeOption None = new(nameof(None), 4);

    private RestrictPurgeOption(string name, int value) : base(name, value) { }
}

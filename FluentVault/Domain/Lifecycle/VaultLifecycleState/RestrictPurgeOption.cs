using FluentVault.Domain.Common;

namespace FluentVault;

public class RestrictPurgeOption : BaseType
{
    public static readonly RestrictPurgeOption All = new(nameof(All));
    public static readonly RestrictPurgeOption FirstAndLast = new(nameof(FirstAndLast));
    public static readonly RestrictPurgeOption Last = new(nameof(Last));
    public static readonly RestrictPurgeOption None = new(nameof(None));

    private RestrictPurgeOption(string value) : base(value) { }

    public static RestrictPurgeOption Parse(string value)
        => Parse(value, new[] { All, FirstAndLast, Last, None });
}

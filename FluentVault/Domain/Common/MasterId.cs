
using FluentVault.Common;

namespace FluentVault;
public class MasterId : GenericId<long>
{
    public MasterId(long value) : base(value) { }

    public static MasterId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse master ID."));
}

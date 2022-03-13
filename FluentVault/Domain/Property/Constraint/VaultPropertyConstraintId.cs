
using FluentVault.Common;

namespace FluentVault;
public class VaultPropertyConstraintId : VaultGenericId<long>
{
    public VaultPropertyConstraintId(long value) : base(value) { }

    public static VaultPropertyConstraintId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse property constraint ID."));
}

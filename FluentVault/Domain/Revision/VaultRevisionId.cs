
using FluentVault.Common;

namespace FluentVault;
public class VaultRevisionId : VaultGenericId<long>
{
    public VaultRevisionId(long value) : base(value) { }

    public static VaultRevisionId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse revision ID."));
}

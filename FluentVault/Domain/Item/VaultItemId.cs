
using FluentVault.Common;

namespace FluentVault;
public class VaultItemId : VaultGenericId<long>
{
    public VaultItemId(long value) : base(value) { }

    public static VaultItemId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse item ID."));
}

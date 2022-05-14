
using FluentVault.Common;

namespace FluentVault;
public class VaultItemId : VaultGenericId<long>
{
    public VaultItemId(long value) : base(value) { }

    public static implicit operator VaultEntityId(VaultItemId itemId) => new(itemId.Value);

    public static VaultItemId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse item ID."));
}


using FluentVault.Common;

namespace FluentVault;
public class VaultContentSourceId : VaultGenericId<long>
{
    public VaultContentSourceId(long value) : base(value) { }

    public static VaultContentSourceId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse content source ID."));
}

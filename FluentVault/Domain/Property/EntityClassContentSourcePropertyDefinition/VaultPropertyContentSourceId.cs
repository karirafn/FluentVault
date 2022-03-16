
using FluentVault.Common;

namespace FluentVault;
public class VaultPropertyContentSourceId : VaultGenericId<long>
{
    public VaultPropertyContentSourceId(long value) : base(value) { }

    public static VaultPropertyContentSourceId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse content source ID."));
}

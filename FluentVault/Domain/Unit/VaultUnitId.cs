
using FluentVault.Common;

namespace FluentVault;
public class VaultUnitId : VaultGenericId<long>
{
    public VaultUnitId(long value) : base(value) { }

    public static VaultFileId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse unit ID."));
}

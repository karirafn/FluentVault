
using FluentVault.Common;

namespace FluentVault;
public class VaultFileId : GenericId<long>
{
    public VaultFileId(long value) : base(value) { }

    public static VaultFileId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse category ID."));
}


using FluentVault.Common;

namespace FluentVault;
public class VaultFileAssociationId : VaultGenericId<long>
{
    public VaultFileAssociationId(long value) : base(value) { }

    public static VaultFileAssociationId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse file association ID."));
}


using FluentVault.Common;

namespace FluentVault;
public class VaultNumberingSchemeId : VaultGenericId<long>
{
    public VaultNumberingSchemeId(long value) : base(value) { }

    public static VaultFileId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse numbering scheme ID."));
}

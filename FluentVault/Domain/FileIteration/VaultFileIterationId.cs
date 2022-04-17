
using FluentVault.Common;

namespace FluentVault;
public class VaultFileIterationId : VaultGenericId<long>
{
    public VaultFileIterationId(long value) : base(value) { }

    public static VaultFileIterationId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse file iteration ID."));
}

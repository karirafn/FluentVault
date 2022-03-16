using FluentVault.Common;

namespace FluentVault;
public class VaultInstanceId : VaultGenericId<long>
{
    public VaultInstanceId(long value) : base(value) { }

    public static VaultInstanceId Parse(string value)
        => new(long.TryParse(value, out long id)
            ? id
            : throw new KeyNotFoundException("Failed to parse vault ID."));
}

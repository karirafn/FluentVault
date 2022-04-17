
using FluentVault.Common;

namespace FluentVault;
internal class VaultTicket : VaultGenericId<Guid>
{
    public VaultTicket(Guid value) : base(value) { }

    public static VaultTicket Parse(string value)
        => new(Guid.TryParse(value, out Guid id)
            ? id
            : throw new KeyNotFoundException("Failed to parse ticket."));
}

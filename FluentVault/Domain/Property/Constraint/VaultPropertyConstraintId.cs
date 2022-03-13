using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultPropertyConstraintId : GenericId<long>
{
    public VaultPropertyConstraintId(long value) : base(value) { }

    public static VaultPropertyConstraintId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse property constraint ID."));
}

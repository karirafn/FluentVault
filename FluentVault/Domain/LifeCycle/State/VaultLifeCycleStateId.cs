
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultLifeCycleStateId : GenericId<long>
{
    public VaultLifeCycleStateId(long value) : base(value) { }

    public static VaultLifeCycleStateId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse life cycle state ID."));
}

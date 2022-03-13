using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultLifeCycleTransitionId : GenericId<long>
{
    public VaultLifeCycleTransitionId(long value) : base(value) { }

    public static VaultLifeCycleTransitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse life cycle transition ID."));
}

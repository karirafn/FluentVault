using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class LifeCycleDefinitionId : GenericId<long>
{
    public LifeCycleDefinitionId(long value) : base(value) { }

    public static LifeCycleDefinitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse life cycle definition ID."));
}

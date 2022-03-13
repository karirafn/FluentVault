using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultLifeCycleDefinitionId : GenericId<long>
{
    public VaultLifeCycleDefinitionId(long value) : base(value) { }

    public static VaultLifeCycleDefinitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse life cycle definition ID."));
}

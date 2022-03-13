using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class RevisionDefinitionId : GenericId<long>
{
    public RevisionDefinitionId(long value) : base(value) { }

    public static RevisionDefinitionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse revision definition ID."));
}

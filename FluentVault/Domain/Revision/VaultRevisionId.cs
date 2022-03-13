using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultRevisionId : GenericId<long>
{
    public VaultRevisionId(long value) : base(value) { }

    public static VaultRevisionId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse revision ID."));
}

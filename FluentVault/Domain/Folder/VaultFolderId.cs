
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;
public class VaultFolderId : GenericId<long>
{
    public VaultFolderId(long value) : base(value) { }

    public static VaultFolderId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse folder ID."));
}

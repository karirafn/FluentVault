
using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Folder;
public class FolderId : GenericId<long>
{
    public FolderId(long value) : base(value) { }

    public static FolderId ParseFromAttribute(XElement element, string key)
        => new(long.TryParse(element.GetAttributeValue(key), out long value)
            ? value
            : throw new KeyNotFoundException("Failed to parse folder ID."));
}

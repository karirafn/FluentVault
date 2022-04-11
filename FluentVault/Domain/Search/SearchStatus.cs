using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Domain.Search;
public record SearchStatus(int TotalHits, IndexingStatus IndexingStatus)
{
    internal static SearchStatus Parse(XElement element)
        => new(element.ParseAttributeValue(nameof(TotalHits), int.Parse),
            element.ParseAttributeValue("IndxStatus", x => IndexingStatus.FromName(x)));
}

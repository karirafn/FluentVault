using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class SearchStatusSerializer : XElementSerializer<SearchStatus>
{
    private const string IndxStatus = nameof(IndxStatus);

    public SearchStatusSerializer(XNamespace @namespace) : base("searchstatus", @namespace) { }

    internal override SearchStatus Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(nameof(SearchStatus.TotalHits), int.Parse),
                   element.ParseAttributeValue(IndxStatus, x => IndexingStatus.FromName(x)));
    }

    internal override XElement Serialize(SearchStatus entity)
        => BaseElement.AddAttribute(nameof(SearchStatus.TotalHits), entity.TotalHits)
            .AddAttribute(IndxStatus, entity.IndexingStatus);
}

using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal class SortConditionSerializer : XElementSerializer<SortCondition>
{
    private const string SrchSort = nameof(SrchSort);
    private const string PropDefId = nameof(PropDefId);
    private const string SortAsc = nameof(SortAsc);

    public SortConditionSerializer(XNamespace @namespace) : base(SrchSort, @namespace) { }

    internal override SortCondition Deserialize(XElement element)
        => new(element.ParseAttributeValue(PropDefId, VaultPropertyDefinitionId.Parse),
                element.ParseAttributeValue(SortAsc, bool.Parse));

    internal override XElement Serialize(SortCondition condition)
        => BaseElement
            .AddAttribute(PropDefId, condition.PropertyId)
            .AddAttribute(SortAsc, condition.SortAscending);
}

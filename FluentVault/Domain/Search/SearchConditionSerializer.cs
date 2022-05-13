using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Search;

internal class SearchConditionSerializer : XElementSerializer<SearchCondition>
{
    private const string SrchCond = nameof(SrchCond);
    private const string PropDefId = nameof(PropDefId);
    private const string SrchOper = nameof(SrchOper);
    private const string SrchTxt = nameof(SrchTxt);
    private const string PropTyp = nameof(PropTyp);
    private const string SrchRule = nameof(SrchRule);

    public SearchConditionSerializer(XNamespace @namespace) : base(SrchCond, @namespace) { }

    internal override SearchCondition Deserialize(XElement element)
        => new(element.ParseAttributeValue(PropDefId, x => VaultPropertyDefinitionId.Parse(x)),
            element.ParseAttributeValue(SrchOper, x => SearchOperator.FromValue(int.Parse(x))),
            element.GetAttributeValue(SrchTxt),
            element.ParseAttributeValue(PropTyp, x => SearchPropertyType.FromName(x)),
            element.ParseAttributeValue(SrchRule, x => SearchRule.FromName(x)));

    internal override XElement Serialize(SearchCondition condition)
        => BaseElement
            .AddAttribute(PropDefId, condition.PropertyId)
            .AddAttribute(SrchOper, condition.SearchOperator.Value)
            .AddAttribute(SrchTxt, condition.SearchText)
            .AddAttribute(PropTyp, condition.PropertyType)
            .AddAttribute(SrchRule, condition.SearchRule);
}

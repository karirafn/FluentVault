using System.Text;

namespace FluentVault.Common.Extensions;

internal static class StringBuilderExtensions
{
    internal static StringBuilder AppendElementOpening(this StringBuilder builder, string elementName)
        => builder.Append($"<{elementName}>");

    internal static StringBuilder AppendElementClosing(this StringBuilder builder, string elementName)
        => builder.Append($"</{elementName}>");

    internal static StringBuilder AppendElement(this StringBuilder builder, string elementName, string value)
        => builder.AppendElementOpening(elementName)
                .Append(value)
                .AppendElementClosing(elementName);

    internal static StringBuilder AppendElement(this StringBuilder builder, string elementName, bool value)
        => builder.AppendElement(elementName, value.ToString().ToLower());

    internal static StringBuilder AppendElement(this StringBuilder builder, string elementName, long value)
        => builder.AppendElement(elementName, value.ToString());

    internal static StringBuilder AppendElements(this StringBuilder builder, string elementName, IEnumerable<string> values)
        => values.Aggregate(builder, (b, id) => b.AppendElement(elementName, id));

    internal static StringBuilder AppendElements(this StringBuilder builder, string elementName, IEnumerable<bool> values)
        => builder.AppendElements(elementName, values.Select(x => x.ToString().ToLower()));

    internal static StringBuilder AppendElements(this StringBuilder builder, string elementName, IEnumerable<long> values)
        => builder.AppendElements(elementName, values.Select(x => x.ToString()));

    internal static StringBuilder AppendElementWithAttributes(this StringBuilder builder, string elementName, IDictionary<string, string> attributes, bool isSelfClosing = false)
        => builder
            .Append($"<{elementName}")
            .AppendAttributes(attributes)
            .Append($"{(isSelfClosing ? "/" : "")}>");

    internal static StringBuilder AppendElementsWithAttributes(this StringBuilder builder, string elementName, IEnumerable<IDictionary<string, string>> elementAttributes)
        => elementAttributes.Aggregate(builder,
            (builder, attributes)
                => builder.AppendElementWithAttributes(elementName, attributes, isSelfClosing: true));

    internal static StringBuilder AppendNestedElementsWithAttributes(this StringBuilder builder, string parentName, string childName, IEnumerable<IDictionary<string, string>> attributes)
        => builder
            .AppendElementOpening(parentName)
            .AppendElementsWithAttributes(childName, attributes)
            .AppendElementClosing(parentName);

    internal static StringBuilder AppendElementWithAttribute(this StringBuilder builder, string elementName, string attributeName, string attributeValue, bool isSelfClosing = false)
        => builder.AppendElementWithAttributes(elementName, new Dictionary<string, string> { { attributeName, attributeValue } }, isSelfClosing);

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentName, string childName, IEnumerable<string> values)
        => builder
            .AppendElementOpening(parentName)
            .AppendElements(childName, values)
            .AppendElementClosing(parentName);

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentName, string childName, IEnumerable<bool> values)
        => builder.AppendNestedElements(parentName, childName, values.Select(x => x.ToString().ToLower()));

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentName, string childName, IEnumerable<long> values)
        => builder.AppendNestedElements(parentName, childName, values.Select(x => x.ToString()));

    private static StringBuilder AppendAttributes(this StringBuilder builder, IDictionary<string, string> attributes)
        => attributes.Aggregate(builder,
            (builder, attribute)
                => builder.Append($@" {attribute.Key}=""{attribute.Value}"""));
}

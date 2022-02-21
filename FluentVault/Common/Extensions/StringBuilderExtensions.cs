using System.Text;

namespace FluentVault.Common.Extensions;

internal static class StringBuilderExtensions
{
    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, long value)
        => builder.AppendLine($"<{tag}>{value}</{tag}>");

    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, string value)
        => builder.AppendLine($"<{tag}>{value}</{tag}>");

    internal static StringBuilder AppendOpeningTag(this StringBuilder builder, string tag)
        => builder.AppendLine($"<{tag}");

    internal static StringBuilder AppendClosingTag(this StringBuilder builder, string tag)
        => builder.AppendLine($"</{tag}");

    internal static StringBuilder AppendElementWithAttribute(this StringBuilder builder, string elementName, string attributeName, string attributeValue, bool isSelfClosing = false)
        => builder.AppendLine($@"<{elementName} {attributeName}=""{attributeValue}""{(isSelfClosing ? "/" : "")}>");

    internal static StringBuilder AppendElementArray(this StringBuilder builder, string tag, IEnumerable<long> values)
        => values.Aggregate(builder, (b, id) => b.AppendElement(tag, id));

    internal static StringBuilder AppendNestedElementArray(this StringBuilder builder, string parentTag, string childTag, IEnumerable<long> values)
        => builder
            .AppendOpeningTag(parentTag)
            .AppendElementArray(childTag, values)
            .AppendClosingTag(parentTag);
}

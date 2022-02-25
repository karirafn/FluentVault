using System.Text;

namespace FluentVault.Common.Extensions;

internal static class StringBuilderExtensions
{
    internal static StringBuilder AppendElementOpening(this StringBuilder builder, string tag)
        => builder.Append('<')
            .Append(tag)
            .Append('>');

    internal static StringBuilder AppendElementClosing(this StringBuilder builder, string tag)
        => builder.Append("</")
            .Append(tag)
            .Append('>');

    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, string value)
        => builder.AppendElementOpening(tag)
                .Append(value)
                .AppendElementClosing(tag);

    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, bool value)
        => builder.AppendElement(tag, value.ToString().ToLower());

    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, Guid value)
        => builder.AppendElement(tag, value.ToString());

    internal static StringBuilder AppendElement(this StringBuilder builder, string tag, long value)
        => builder.AppendElement(tag, value.ToString());

    internal static StringBuilder AppendElements(this StringBuilder builder, string tag, IEnumerable<string> values)
        => values.Aggregate(builder, (b, id) => b.AppendElement(tag, id));

    internal static StringBuilder AppendElements(this StringBuilder builder, string tag, IEnumerable<bool> values)
        => builder.AppendElements(tag, values.Select(x => x.ToString().ToLower()));

    internal static StringBuilder AppendElements(this StringBuilder builder, string tag, IEnumerable<long> values)
        => builder.AppendElements(tag, values.Select(x => x.ToString()));

    internal static StringBuilder AppendElementWithAttributes(this StringBuilder builder, string tag, IDictionary<string, string> attributes, bool isSelfClosing = false)
        => builder.Append('<')
            .Append(tag)
            .AppendAttributes(attributes)
            .Append((isSelfClosing ? '/' : ""))
            .Append('>');

    internal static StringBuilder AppendElementsWithAttributes(this StringBuilder builder, string tag, IEnumerable<IDictionary<string, string>> elementAttributes)
        => elementAttributes.Aggregate(builder,
            (builder, attributes)
                => builder.AppendElementWithAttributes(tag, attributes, isSelfClosing: true));

    internal static StringBuilder AppendNestedElementsWithAttributes(this StringBuilder builder, string parentTag, string childTag, IEnumerable<IDictionary<string, string>> attributes)
        => builder
            .AppendElementOpening(parentTag)
            .AppendElementsWithAttributes(childTag, attributes)
            .AppendElementClosing(parentTag);

    internal static StringBuilder AppendElementWithAttribute(this StringBuilder builder, string tag, string attributeName, string attributeValue, bool isSelfClosing = false)
        => builder.AppendElementWithAttributes(tag, new Dictionary<string, string> { { attributeName, attributeValue } }, isSelfClosing);

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentTag, string childTag, IEnumerable<string> values)
        => builder
            .AppendElementOpening(parentTag)
            .AppendElements(childTag, values)
            .AppendElementClosing(parentTag);

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentTag, string childTag, IEnumerable<bool> values)
        => builder.AppendNestedElements(parentTag, childTag, values.Select(x => x.ToString().ToLower()));

    internal static StringBuilder AppendNestedElements(this StringBuilder builder, string parentName, string childName, IEnumerable<long> values)
        => builder.AppendNestedElements(parentName, childName, values.Select(x => x.ToString()));

    internal static StringBuilder AppendRequestBody(this StringBuilder builder, StringBuilder innerBody, Guid ticket = new(), long userId = 0)
        => builder.AppendElementWithAttribute("s:Envelope", "xmlns:s", @"http://schemas.xmlsoap.org/soap/envelope/")
            .AppendHeaderBody(ticket, userId)
            .AppendElementWithAttributes("s:Body", _bodyHeaderNamespaces)
            .Append(innerBody)
            .AppendElementClosing("s:Body")
            .AppendElementClosing("s:Envelope");

    internal static StringBuilder AppendHeaderBody(this StringBuilder builder, Guid ticket, long userId)
        => (ticket != Guid.Empty && userId > 0)
        ? builder.AppendElementOpening("s:Header")
            .AppendElementWithAttributes("SecurityHeader", _securityHeaderNamespaces)
            .AppendElement("Ticket", ticket)
            .AppendElement("UserId", userId)
            .AppendElementClosing("SecurityHeader")
            .AppendElementClosing("s:Header")
        : builder;

    private static readonly IDictionary<string, string> _bodyHeaderNamespaces = new Dictionary<string, string>
    {
        { "xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance" },
        { "xmlns:xsd", @"http://www.w3.org/2001/XMLSchema" },
    };

    private static readonly IDictionary<string, string> _securityHeaderNamespaces = new Dictionary<string, string>
    {
        { "xmlns", @"http://AutodeskDM/Services" },
        { "xmlns:xsd", @"http://AutodeskDM/Services" },
        { "xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance" },
    };

    private static StringBuilder AppendAttributes(this StringBuilder builder, IDictionary<string, string> attributes)
        => attributes.Aggregate(builder,
            (builder, attribute)
                => builder.AppendAttribute(attribute.Key, attribute.Value));

    private static StringBuilder AppendAttribute(this StringBuilder builder, string name, string value)
        => builder.Append(' ')
            .Append(name)
            .Append(@"=""")
            .Append(value)
            .Append('"');
}

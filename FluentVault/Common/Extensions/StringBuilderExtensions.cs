using System.Text;

using FluentVault.Domain;
using FluentVault.Domain.SOAP;

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

    internal static StringBuilder AppendRequestBodyOpening(this StringBuilder builder, VaultSessionCredentials session)
        => builder.AppendElementWithAttribute("s:Envelope", "xmlns:s", @"http://schemas.xmlsoap.org/soap/envelope/")
            .AppendHeaderBody(session)
            .AppendElementWithAttributes("s:Body", _bodyHeaderNamespaces);

    internal static StringBuilder AppendRequestBodyClosing(this StringBuilder builder)
        => builder.AppendElementClosing("s:Body")
            .AppendElementClosing("s:Envelope");

    internal static StringBuilder AppendHeaderBody(this StringBuilder builder, VaultSessionCredentials session)
        => (session.Ticket != Guid.Empty && session.UserId > 0)
        ? builder.AppendElementOpening("s:Header")
            .AppendElementWithAttributes("SecurityHeader", _securityHeaderNamespaces)
            .AppendElement("Ticket", session.Ticket)
            .AppendElement("UserId", session.UserId)
            .AppendElementClosing("SecurityHeader")
            .AppendElementClosing("s:Header")
        : builder;

    internal static StringBuilder SoapActionStringBuilder(this StringBuilder builder, SoapRequestData request)
        => builder.Append('"')
            .AppendNamespace(request)
            .Append('"')
            .Append(request.Service)
            .Append('/')
            .Append(request.Name)
            .Append('"');

    internal static StringBuilder AppendNamespace(this StringBuilder builder, SoapRequestData request)
        => builder.Append("http://AutodeskDM/")
            .Append(request.Namespace);

    internal static StringBuilder AppendRequestUri(this StringBuilder builder, SoapRequestData request)
        => builder.Append("AutodeskDM/Services/")
            .Append(request.Version)
            .Append('/')
            .Append(request.Service)
            .Append(".svc")
            .AppendRequestCommand(request);

    internal static StringBuilder AppendRequestCommand(this StringBuilder builder, SoapRequestData request)
        => string.IsNullOrEmpty(request.Command)
        ? builder
        : builder.Append("?op=")
            .Append(request.Name)
            .Append("&currentCommand=")
            .Append(request.Command);

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

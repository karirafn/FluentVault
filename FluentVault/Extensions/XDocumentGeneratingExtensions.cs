using System.Xml.Linq;

using FluentVault.Domain.SecurityHeader;

namespace FluentVault.Extensions;

internal static class XDocumentGeneratingExtensions
{
    private static readonly XNamespace _xsd = "http://www.w3.org/2001/XMLSchema";
    private static readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly XNamespace _envelope = "http://schemas.xmlsoap.org/soap/envelope/";

    internal static XElement AddAttribute(this XElement element, XName name, object? value)
    {
        if (value is null)
            return element;

        element.Add(new XAttribute(name, value));

        return element;
    }

    internal static XElement AddAttributes(this XElement element, IDictionary<string, object>? attributes)
    {
        if (attributes is null)
            return element;

        foreach (KeyValuePair<string, object> attribute in attributes)
            _ = element.AddAttribute(attribute.Key, attribute.Value);

        return element;
    }

    internal static XElement AddElement(this XElement element, XElement? child)
    {
        element.Add(child);

        return element;
    }

    internal static XElement AddElement(this XElement element, XNamespace ns, string childName, object? childValue)
        => element.AddElement(new XElement(ns + childName, childValue));

    internal static XElement AddElements(this XElement element, XNamespace ns, string childName, IEnumerable<object>? childValues)
    {
        if (childValues is null)
            return element;

        foreach (var childValue in childValues)
            _ = element.AddElement(ns, childName, childValue);

        return element;
    }

    internal static XElement AddElements(this XElement element, IEnumerable<XElement?>? elements = null)
    {
        if (elements is not null)
        {
            elements = elements.Where(content => content is not null);

            if (elements.Any())
                element.Add(elements);
        }

        return element;
    }

    internal static XElement AddNestedElements(this XElement element, XNamespace ns, string childName, string nestedName, IEnumerable<object>? nestedValues)
    {
        if (nestedValues is null)
            return element;

        XElement child = new(ns + childName);

        foreach (var value in nestedValues)
            _ = child.AddElement(ns, nestedName, value);

        element.Add(child);

        return element;
    }

    internal static XElement AddElementWithAttributes(this XElement element, XNamespace ns, string childName, IDictionary<string, object>? attributes)
    {
        if (attributes is null)
            return element;

        XElement child = new XElement(ns + childName).AddAttributes(attributes);

        element.Add(child);

        return element;
    }

    internal static XElement AddElementsWithAttributes(this XElement element, XNamespace ns, string childName, IEnumerable<IDictionary<string, object>>? attributeSets)
    {
        if (attributeSets is null)
            return element;

        foreach (IDictionary<string, object> attributes in attributeSets)
            _ = element.AddElementWithAttributes(ns, childName, attributes);

        return element;
    }

    internal static XElement AddNestedElementsWithAttributes(this XElement element, XNamespace ns, string rootName, string childName, IEnumerable<IDictionary<string, object>>? attributeSets)
    {
        if (attributeSets is null)
            return element;

        XElement root = new XElement(ns + rootName).AddElementsWithAttributes(ns, childName, attributeSets);

        element.Add(root);

        return element;
    }

    internal static XDocument AddResponse(this XDocument document, string operation, XNamespace @namespace, XElement result, IEnumerable<XElement?>? responseContent = null)
    {
        XElement response = new(@namespace + $"{operation}Response");

        response.AddElements(responseContent)
            .AddElement(result);

        document.AddRequestBody(response);

        return document;
    }

    internal static XDocument AddResponseContent(this XDocument document, string operation, XNamespace @namespace, IEnumerable<XElement?>? responseContent = null, IEnumerable<XElement?>? resultContent = null)
    {
        XElement result = new(@namespace + $"{operation}Result");

        result.AddElements(resultContent);
        document.AddResponse(operation, @namespace, result, responseContent);

        return document;
    }

    internal static XDocument AddResponseContent(this XDocument document, string operation, XNamespace @namespace, XElement responseContent, IEnumerable<XElement?>? resultContent = null)
        => document.AddResponseContent(operation, @namespace, new XElement[] { responseContent }, resultContent);

    internal static XDocument AddRequestBody(this XDocument document, XElement content, VaultSecurityHeader? securityHeader = null)
    {
        XElement envelope = document.AddEnvelope();

        if (securityHeader is not null)
        {
            VaultSecurityHeaderSerializer serializer = new();
            envelope.AddHeader().AddElement(serializer.Serialize(securityHeader));
        }

        envelope.AddBody()
            .Add(content);

        return document;
    }

    internal static XElement AddBody(this XElement element)
    {
        XElement body = new XElement(_envelope + "Body")
            .AddXmlSchema();

        element.Add(body);

        return body;
    }

    internal static XElement AddHeader(this XElement element)
    {
        XElement header = new(_envelope + "Header");

        element.Add(header);

        return header;
    }

    internal static XElement AddEnvelope(this XDocument document)
    {
        XElement envelope = new XElement(_envelope + "Envelope")
            .AddNamespace(XNamespace.Xmlns + "s", _envelope);

        document.Add(envelope);

        return envelope;
    }

    internal static XElement AddXmlSchema(this XElement element)
        => element.AddNamespace(XNamespace.Xmlns + "xsd", _xsd)
            .AddNamespace(XNamespace.Xmlns + "xsi", _xsi);

    internal static XElement AddNamespace(this XElement element, XName name, XNamespace ns)
    {
        element.Add(new XAttribute(name, ns.NamespaceName));

        return element;
    }
}

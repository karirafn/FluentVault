using System.Xml.Linq;

namespace FluentVault.Extensions;

internal static class XDocumentGeneratingExtensions
{
    private static readonly XNamespace _xsd = "http://www.w3.org/2001/XMLSchema";
    private static readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly XNamespace _envelope = "http://schemas.xmlsoap.org/soap/envelope/";
    private static readonly XNamespace _autodesk = "http://AutodeskDM/Services";

    internal static XElement AddAttribute(this XElement element, XName name, object value)
    {
        element.Add(new XAttribute(name, value));

        return element;
    }

    internal static XElement AddAttributes(this XElement element, IDictionary<string, object> attributes)
    {
        foreach (KeyValuePair<string, object> attribute in attributes)
            _ = element.AddAttribute(attribute.Key, attribute.Value);

        return element;
    }

    internal static XElement AddElement(this XElement element, XNamespace ns, string childName, object childValue)
    {
        element.Add(new XElement(ns + childName, childValue));

        return element;
    }

    internal static XElement AddElement(this XElement element, XElement child)
    {
        element.Add(child);

        return element;
    }

    internal static XElement AddElements(this XElement element, XNamespace ns, string childName, IEnumerable<object> childValues)
    {
        foreach (var childValue in childValues)
            _ = element.AddElement(ns, childName, childValue);

        return element;
    }

    internal static XElement AddNestedElements(this XElement element, XNamespace ns, string childName, string nestedName, IEnumerable<object> nestedValues)
    {
        XElement child = new(ns + childName);

        foreach (var value in nestedValues)
            _ = child.AddElement(ns, nestedName, value);

        element.Add(child);

        return element;
    }

    internal static XElement AddElementWithAttributes(this XElement element, XNamespace ns, string childName, IDictionary<string, object> attributes)
    {
        XElement child = new XElement(ns + childName).AddAttributes(attributes);

        element.Add(child);

        return element;
    }

    internal static XElement AddElementsWithAttributes(this XElement element, XNamespace ns, string childName, IEnumerable<IDictionary<string, object>> attributeSets)
    {
        foreach (IDictionary<string, object> attributes in attributeSets)
            _ = element.AddElementWithAttributes(ns, childName, attributes);

        return element;
    }

    internal static XElement AddNestedElementsWithAttributes(this XElement element, XNamespace ns, string rootName, string childName, IEnumerable<IDictionary<string, object>> attributeSets)
    {
        XElement root = new XElement(ns + rootName).AddElementsWithAttributes(ns, childName, attributeSets);

        element.Add(root);

        return element;
    }

    internal static XDocument AddResponseContent(this XDocument document, string operation, XNamespace @namespace, IEnumerable<XElement> responseContent, IEnumerable<XElement?>? resultContent)
    {
        XElement response = new(@namespace + $"{operation}Response");
        XElement result = new(@namespace + $"{operation}Result");

        if (resultContent is not null)
        {
            resultContent = resultContent.Where(content => content is not null);

            if (resultContent.Any())
                result.Add(resultContent);
        }

        result.Add(responseContent);
        response.Add(result);
        document.AddRequestBody(new(), response);

        return document;
    }

    internal static XDocument AddResponseContent(this XDocument document, string operation, XNamespace @namespace, XElement responseContent, IEnumerable<XElement>? resultContent)
        => document.AddResponseContent(operation, @namespace, new XElement[] { responseContent }, resultContent);

    internal static XDocument AddRequestBody(this XDocument document, VaultSessionCredentials session, XElement content)
    {
        XElement envelope = document.AddEnvelope();

        if (session.Ticket != Guid.Empty && session.UserId > 0)
            envelope.AddHeader().AddSecurityHeader(session);

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

    internal static XElement AddSecurityHeader(this XElement element, VaultSessionCredentials session)
    {
        XElement ticket = new(_autodesk + "Ticket", session.Ticket);
        XElement userId = new(_autodesk + "UserId", session.UserId);

        XElement securityHeader = new XElement(_autodesk + "SecurityHeader")
            .AddXmlSchema();

        securityHeader.Add(ticket);
        securityHeader.Add(userId);

        element.Add(securityHeader);

        return securityHeader;
    }

    internal static XElement AddHeader(this XElement element)
    {
        XElement header = new(_envelope + "Header");

        element.Add(header);

        return element;
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

using System.Xml.Linq;

using FluentVault.Domain;

namespace FluentVault.Common.Extensions;

internal static class XDocumentGeneratingExtensions
{
    private static readonly XNamespace _xsd = "http://www.w3.org/2001/XMLSchema";
    private static readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly XNamespace _envelope = "http://schemas.xmlsoap.org/soap/envelope/";
    private static readonly XNamespace _autodesk = "http://AutodeskDM/Services";

    internal static void AddAttribute(this XElement element, XName name, string value)
        => element.Add(new XAttribute(name, value));

    internal static void AddAttributes(this XElement element, IDictionary<string, string> attributes)
    {
        foreach (KeyValuePair<string, string> attribute in attributes)
            element.AddAttribute(attribute.Key, attribute.Value);
    }

    internal static void AddElement(this XElement parent, XNamespace ns, string childName, object childValue)
        => parent.Add(new XElement(ns + childName, childValue));

    internal static void AddElements(this XElement parent, XNamespace ns, string childName, IEnumerable<string> childValues)
    {
        foreach (var childValue in childValues)
            parent.AddElement(ns, childName, childValue);
    }

    internal static void AddNestedElements(this XElement parent, XNamespace ns, string childName, string nestedName, IEnumerable<string> nestedValues)
    {
        XElement child = new(ns + childName);

        foreach (var value in nestedValues)
            child.AddElement(ns, nestedName, value);

        parent.Add(child);
    }

    internal static void AddElementWithAttributes(this XElement parent, XNamespace ns, string childName, IDictionary<string, string> attributes)
    {
        XElement child = new(ns + childName);
        child.AddAttributes(attributes);

        parent.Add(child);
    }

    internal static void AddElementsWithAttributes(this XElement parent, XNamespace ns, string childName, IEnumerable<IDictionary<string, string>> attributeSets)
    {
        foreach (IDictionary<string, string> attributes in attributeSets)
            parent.AddElementWithAttributes(ns, childName, attributes);
    }

    internal static XDocument AddRequestBody(this XDocument document, VaultSessionCredentials session, XElement content)
    {
        XElement envelope = document.AddEnvelope();

        if (session.Ticket != Guid.Empty && session.UserId > 0)
            envelope.AddHeader().AddSecurityHeader(session);

        envelope.AddBody().Add(content);

        return document;
    }

    private static XElement AddBody(this XElement element)
    {
        XElement body = new(_envelope + "Body");
        body.AddXmlSchema();

        element.Add(body);

        return body;
    }

    private static XElement AddSecurityHeader(this XElement element, VaultSessionCredentials session)
    {
        XElement securityHeader = new(_autodesk + "SecurityHeader");
        XElement ticket = new(_autodesk + "Ticket", session.Ticket);
        XElement userId = new(_autodesk + "UserId", session.UserId);

        securityHeader.AddXmlSchema();
        securityHeader.Add(ticket);
        securityHeader.Add(userId);

        element.Add(securityHeader);

        return securityHeader;
    }

    private static XElement AddHeader(this XElement element)
    {
        XElement header = new(_envelope + "Header");

        element.Add(header);

        return header;
    }

    private static XElement AddEnvelope(this XDocument document)
    {
        XElement envelope = new(_envelope + "Envelope");
        envelope.AddNamespace(XNamespace.Xmlns + "s", _envelope);

        document.Add(envelope);

        return envelope;
    }

    private static void AddXmlSchema(this XElement element)
    {
        element.AddNamespace(XNamespace.Xmlns + "xsd", _xsd);
        element.AddNamespace(XNamespace.Xmlns + "xsi", _xsi);
    }

    private static void AddNamespace(this XElement element, XName name, XNamespace ns)
        => element.Add(new XAttribute(name, ns.NamespaceName));
}

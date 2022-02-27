using System.Xml.Linq;

using FluentVault.Domain;

namespace FluentVault.Common.Extensions;

internal static class XDocumentGeneratingExtensions
{
    private static readonly XNamespace _envelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private static readonly XNamespace _xsdNamespace = "http://www.w3.org/2001/XMLSchema";
    private static readonly XNamespace _xsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly XNamespace _autodeskNamespace = "http://AutodeskDM/Services";

    private static readonly XAttribute _xsdAttribute = new(XNamespace.Xmlns + "xsd", _xsdNamespace.NamespaceName);
    private static readonly XAttribute _xsiAttribute = new(XNamespace.Xmlns + "xsi", _xsiNamespace.NamespaceName);

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
        XElement body = new(_envelopeNamespace + "Body");
        body.Add(_xsdAttribute);
        body.Add(_xsiAttribute);

        element.Add(body);

        return body;
    }

    private static XElement AddSecurityHeader(this XElement element, VaultSessionCredentials session)
    {
        XElement securityHeader = new(_autodeskNamespace + "SecurityHeader");
        XElement ticket = new(_autodeskNamespace + "Ticket", session.Ticket);
        XElement userId = new(_autodeskNamespace + "UserId", session.UserId);
        
        securityHeader.Add(_xsdAttribute);
        securityHeader.Add(_xsiAttribute);
        securityHeader.Add(ticket);
        securityHeader.Add(userId);

        element.Add(securityHeader);

        return securityHeader;
    }

    private static XElement AddHeader(this XElement element)
    {
        XElement header = new(_envelopeNamespace + "Header");

        element.Add(header);

        return header;
    }

    private static XElement AddEnvelope(this XDocument document)
    {
        XAttribute elementAttribute = new(XNamespace.Xmlns + "s", _envelopeNamespace.NamespaceName);
        XElement envelope = new(_envelopeNamespace + "Envelope");
        envelope.Add(elementAttribute);

        document.Add(envelope);

        return envelope;
    }
}

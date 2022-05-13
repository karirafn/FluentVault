using System.Xml.Linq;

using FluentVault.Domain.SecurityHeader;
using FluentVault.Extensions;

namespace FluentVault.Common;
internal class VaultRequestSerializer
{
    private static readonly XNamespace _xsd = "http://www.w3.org/2001/XMLSchema";
    private static readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly XNamespace _envelope = "http://schemas.xmlsoap.org/soap/envelope/";

    public static XDocument Serialize(VaultRequest request, VaultSecurityHeader? securityHeader = null, Action<XElement, XNamespace>? contentBuilder = null)
    {
        XElement content = GetContentElement(request);

        contentBuilder?.Invoke(content, request.XNamespace);

        return GetMessage(content, securityHeader);
    }

    public static XElement GetContentElement(VaultRequest request, string suffix = "")
        => new(request.XNamespace + $"{request.Operation}{suffix}");

    private static XDocument GetMessage(XElement content, VaultSecurityHeader? securityHeader)
        => new XDocument().AddElement(GetMessageElement(content, securityHeader));

    private static XElement GetMessageElement(XElement content, VaultSecurityHeader? securityHeader)
        => GetEnvelopeElement(securityHeader)
            .AddElement(GetBodyElement(content));

    private static XElement GetBodyElement(XElement content)
        => new XElement(_envelope + "Body")
            .AddAttribute(XNamespace.Xmlns + "xsd", _xsd)
            .AddAttribute(XNamespace.Xmlns + "xsi", _xsi)
            .AddElement(content);

    private static XElement GetEnvelopeElement(VaultSecurityHeader? securityHeader)
        => new XElement(_envelope + "Envelope")
            .AddAttribute(XNamespace.Xmlns + "s", _envelope)
            .AddElement(GetHeaderElement(securityHeader));

    private static XElement? GetHeaderElement(VaultSecurityHeader? securityHeader)
        => securityHeader is not null
        ? new XElement(_envelope + "Header")
            .AddElement(new VaultSecurityHeaderSerializer().Serialize(securityHeader))
        : null;
}

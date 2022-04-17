using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.SecurityHeader;
internal class VaultSecurityHeaderSerializer : XElementSerializer<VaultSecurityHeader>
{
    private const string SecurityHeader = nameof(SecurityHeader);

    public VaultSecurityHeaderSerializer() : base(SecurityHeader, "http://AutodeskDM/Services")
    {
    }

    internal override VaultSecurityHeader Deserialize(XElement element)
        => new(element.ParseElementValue(nameof(VaultSecurityHeader.Ticket), VaultTicket.Parse),
            element.ParseElementValue(nameof(VaultSecurityHeader.UserId), VaultUserId.Parse));

    internal override XElement Serialize(VaultSecurityHeader header)
        => BaseElement
            .AddXmlSchema()
            .AddElement(Namespace, nameof(VaultSecurityHeader.Ticket), header.Ticket)
            .AddElement(Namespace, nameof(VaultSecurityHeader.UserId), header.UserId);
}

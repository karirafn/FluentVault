using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Property;
internal class VaultPropertyListValueSerializer : XElementSerializer<string>
{
    private const string ListVal = nameof(ListVal);

    public VaultPropertyListValueSerializer(XNamespace @namespace) : base(ListVal, @namespace) { }

    internal override string Deserialize(XElement element)
        => element.Value;

    internal override XElement Serialize(string value)
    {
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XElement element = new(Namespace + ListVal, value);
        element.AddNamespace(xsi + "type", "xsd:string");

        return element;
    }
}

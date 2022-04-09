using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Property;
public class VaultPropertyListValueFixture : VaultEntityFixture<string>
{
    public VaultPropertyListValueFixture(XNamespace @namespace) : base(@namespace)
    {
    }

    public XElement ParseXElement(IEnumerable<string> values)
        => ParseXElement("ListValArray", values);

    public override XElement ParseXElement(string value)
    {
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XElement element = new(Namespace + "ListVal", value);
        element.AddNamespace(xsi + "type", "xsd:string");

        return element;
    }
}

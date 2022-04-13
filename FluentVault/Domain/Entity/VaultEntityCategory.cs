using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

public record VaultEntityCategory(VaultCategoryId Id, string Name);

internal class VaultEntityCategorySerializer : XElementSerializer<VaultEntityCategory>
{
    public VaultEntityCategorySerializer(XNamespace @namespace) : base("Cat", @namespace)
    {
    }

    internal override VaultEntityCategory Deserialize(XElement element)
        => new(element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.GetAttributeValue("CatName"));

    internal override XElement Serialize(VaultEntityCategory category)
        => BaseElement
            .AddAttribute("CatId", category.Id)
            .AddAttribute("CatName", category.Name);
}

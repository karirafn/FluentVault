using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultEntityCategorySerializer : XElementSerializer<VaultEntityCategory>
{
    private const string Cat = nameof(Cat);
    private const string CatId = nameof(CatId);
    private const string CatName = nameof(CatName);

    public VaultEntityCategorySerializer(XNamespace @namespace) : base(Cat, @namespace) { }

    internal override VaultEntityCategory Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(CatId, VaultCategoryId.Parse),
                   element.GetAttributeValue(CatName));
    }

    internal override XElement Serialize(VaultEntityCategory category)
        => BaseElement
            .AddAttribute(CatId, category.Id)
            .AddAttribute(CatName, category.Name);
}

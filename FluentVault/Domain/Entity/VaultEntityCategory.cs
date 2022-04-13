using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

public class VaultEntityCategory : XElementParser<VaultEntityCategory>
{
    public VaultEntityCategory(VaultCategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public VaultCategoryId Id { get; }
    public string Name { get; }

    protected internal override VaultEntityCategory Parse(XElement element)
        => new(element.ParseAttributeValue("CatId", VaultCategoryId.Parse),
            element.GetAttributeValue("CatName"));
}

using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.Category;
public class VaultCategoryFixture : VaultEntityRequestFixture<VaultCategory>
{
    public VaultCategoryFixture() : base("GetCategoryConfigurationsByBehaviorNames", "http://AutodeskDM/Services/Category/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultCategory category)
    {
        XElement catCfg = new(Namespace + "CatCfg");
        XElement cat = new(Namespace + "Cat");
        cat.AddElement(Namespace, "Id", category.Id);
        cat.AddElement(Namespace, "Name", category.Name);
        cat.AddElement(Namespace, "SysName", category.SystemName);
        cat.AddElement(Namespace, "Color", category.Color);
        cat.AddElement(Namespace, "Descr", category.Description);
        cat.AddNestedElements(Namespace, "EntClassIdArray", "EntClassId", category.EntityClasses.Select(x => x.Name));
        catCfg.Add(cat);

        return catCfg;
    }
}

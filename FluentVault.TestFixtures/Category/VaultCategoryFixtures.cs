
using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultCategory> Files) GetVaultCategoryFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultEntityClass.File);

        return CreateBody<VaultCategory>(fixture, count, "GetCategoryConfigurationsByBehaviorNames", "http://AutodeskDM/Services/Category/1/7/2020/", CreateCategoryBody);
    }

    private static string CreateCategoryBody(VaultCategory category)
        => $@"<CatCfg>
<Cat>
<Id>{category.Id}</Id>
<Name>{category.Name}</Name>
<SysName>{category.SystemName}</SysName>
<Color>{category.Color}</Color>
<Descr>{category.Description}</Descr>
{CreateNestedElementArray("EntClassIdArray", "EntClassId", category.EntityClasses.Select(x => x.Name))}
</Cat>
</CatCfg>";
}

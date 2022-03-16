
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileCategory Category) GetVaultFileCategoryFixture()
    {
        Fixture fixture = new();
        VaultFileCategory category = fixture.Create<VaultFileCategory>();
        string body = CreateFileCategoryBody(category);

        return (body, category);
    }

    private static string CreateFileCategoryBody(VaultFileCategory category)
        => $@"<Cat CatId=""{category.Id}"" CatName=""{category.Name}""/>";
}

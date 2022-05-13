using System.IO;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Search;
public class ItemsShould
{
    private static readonly VaultTestData _testData = new();

    // This test will fail if test part does not have an item assigned
    [Fact]
    public async Task FindSingleFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        VaultItem? result = await sut.Search.Items
            .BySystemProperty(VaultSearchProperty.Number)
            .Containing(Path.GetFileNameWithoutExtension(_testData.TestPartFilename))
            .GetFirstResultAsync();

        // Assert
        result.Should().NotBeNull();
    }
}

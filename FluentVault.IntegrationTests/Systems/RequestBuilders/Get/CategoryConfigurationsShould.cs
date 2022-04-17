using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class CategoryConfigurationsShould
{
    [Fact]
    public async Task ReturnAllCategoryConfigurations()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultCategory> result = await sut.Get.CategoryConfigurations(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

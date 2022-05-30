using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get.Files;
public class ByMasterIdsShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnMultiple()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Get.Files.ByMasterIds(new VaultMasterId[] { _testData.TestPartMasterId });

        // Assert
        result.Should().HaveCountGreaterThan(1);
    }
}

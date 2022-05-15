using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class LatestFileAssociationsByMasterIdsShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnEmptyArray_WhenUsingDefaultArguments()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFileAssociation> result = await sut.Get.Latest.File.Associations
            .ByMasterId(_testData.TestPartMasterId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}

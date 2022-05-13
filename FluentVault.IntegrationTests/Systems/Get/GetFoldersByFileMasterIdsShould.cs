using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class GetFoldersByFileMasterIdsShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFolder> result = await sut.Get.FoldersByFileMasterIds(new VaultMasterId[] { _testData.TestPartMasterId }, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

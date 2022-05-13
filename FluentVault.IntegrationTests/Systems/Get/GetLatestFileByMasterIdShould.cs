using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class GetLatestFileByMasterIdShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        VaultFile result = await sut.Get.LatestFileByMasterId(new(_testData.TestPartMasterId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}

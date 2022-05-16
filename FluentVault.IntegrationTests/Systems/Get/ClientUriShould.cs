using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class ClientUriShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnFile()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        (Uri ThinClient, Uri ThickClient) result = await sut.Get.ThinClientUri(_testData.TestPartMasterId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class GetFileAssociationsShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnEmptyArray_WhenFileHasNoAssociations()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFileAssociation> result = await sut.Get.File.Associations
            .ByFileIterationId(_testData.TestPartIterationId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}

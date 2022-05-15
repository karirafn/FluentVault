using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class RevisionFileAssociationsByIdsShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnEmptyArray_WhenUsingDefaultArguments()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFileAssociation> result = await sut.Get.Revision.File.Associations
            .ByFileIterationId(_testData.TestPartIterationWithoutDrawingId)
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ReturnDrawing_WhenIterationHasDrawing()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFileAssociation> result = await sut.Get.Revision.File.Associations
            .ByFileIterationId(_testData.TestPartIterationWithDrawingId)
            .IncludeRelatedDocumentation
            .ExecuteAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }
}

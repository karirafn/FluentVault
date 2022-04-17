using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders;
public class SearchFilesRequestBuilderShould
{
    private static readonly VaultTestData _testData = new();

    // This test will fail if the Vault has no part files
    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Search.Files
            .ForValueContaining("ipt")
            .InSystemProperty(StringSearchProperty.FileExtension)
            .WithPaging();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    // This test will fail if test part has fewer than 2 versions
    [Fact]
    public async Task FindMultipleVersions_WhenCallingCetAllVersions()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultFile> result = await sut.Search.Files
            .ForValueEqualTo(_testData.TestPartFilename)
            .InSystemProperty(StringSearchProperty.FileName)
            .GetAllVersions()
            .WithPaging();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCountGreaterThan(1);
        result.Should().AllSatisfy(file => file.MasterId.Value.Should().Be(_testData.TestPartMasterId));
    }
}

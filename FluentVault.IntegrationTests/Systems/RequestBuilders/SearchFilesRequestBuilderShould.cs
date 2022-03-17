using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.Requests.Search.Files;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders;
public class SearchFilesRequestBuilderShould : BaseRequestBuilderTest
{
    private static readonly VaultTestData _testData = new();

    // This test will fail if the Vault has no part files
    [Fact]
    public async Task FindFiles()
    {
        // Arrange
        SearchFilesRequestBuilder sut = new(_mediator, _session);

        // Act
        IEnumerable<VaultFile> result = await sut
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
        SearchFilesRequestBuilder sut = new(_mediator, _session);

        // Act
        IEnumerable<VaultFile> result = await sut
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

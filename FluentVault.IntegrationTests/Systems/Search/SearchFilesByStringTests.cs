using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Search;

public class SearchFilesByStringTests : BaseRequestTest
{
    [Fact]
    public async Task SearchFilesByStringEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        string searchValue = _v.TestPartFilename;

        // Act
        VaultFile result = await _vault.Search.Files
            .ForValueEqualTo(searchValue)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception($@"Failed to search for ""{searchValue}""");

        // Assert
        result.MasterId.Should().Be(_v.TestPartMasterId);
    }

    [Fact]
    public async Task SearchFilesByStringContaining_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        string searchValue = _v.TestPartDescription.Split('-').Last();

        // Act
        VaultFile result = await _vault.Search.Files
            .ForValueContaining(searchValue)
            .InUserProperty("Description")
            .SearchSingleAsync()
            ?? throw new Exception($@"Failed to search for ""{searchValue}""");

        // Assert
        result.MasterId.Should().Be(_v.TestPartMasterId);
    }

    [Fact]
    public async Task SearchFilesByStringNotContaining_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        string searchValue = _v.TestPartFilename;

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueNotContaining(searchValue)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchWithPaging();

        // Assert
        result.FirstOrDefault(x => x.MasterId.Equals(_v.TestPartMasterId)).Should().BeNull();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class SearchFilesTests : BaseRequestTest
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
        string searchValue = _v.TestPartDescription.Split('-').First();

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
            .SearchAllAsync();

        // Assert
        result.FirstOrDefault(x => x.MasterId.Equals(_v.TestPartMasterId)).Should().BeNull();
    }

    [Fact]
    public async Task SearchFilesByDateTimeEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        VaultFile file = await _vault.Search.Files
            .ForValueEqualTo(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception($@"File ""{_v.TestPartFilename}"" not found");
        DateTime datetime = file.ModifiedDate;

        // Act
        VaultFile result = await _vault.Search.Files
            .ForValueEqualTo(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchSingleAsync()
            ?? throw new Exception("File not found");

        // Assert
        result.Should().NotBeNull();
        result.ModifiedDate.Should().Be(datetime);
    }

    [Fact]
    public async Task SearchFilesByDateTimeNotEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        VaultFile file = await _vault.Search.Files
            .ForValueEqualTo(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception($@"File ""{_v.TestPartFilename}"" not found");
        DateTime datetime = file.ModifiedDate;

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueNotEqualTo(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchAllAsync();

        // Assert
        result.FirstOrDefault(x => x.ModifiedDate.Equals(file.ModifiedDate)).Should().BeNull();
    }

    [Fact]
    public async Task SearchFilesByDateTimeGreaterThanOrEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        var datetime = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueGreaterThanOrEqualTo(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchAllAsync();

        // Assert
        result.Should().NotBeEmpty();
        _ = result.Select(x => x.ModifiedDate.Should().BeOnOrAfter(datetime));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Search;

public class SearchFilesByDateTimeTests : BaseRequestTest
{
    [Fact]
    public async Task SearchFilesByDateTimeEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        VaultFile file = await _vault.Search.Files
            .ForValueEqualTo(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception($@"File ""{_v.TestPartFilename}"" not found");
        DateTime searchValue = file.ModifiedDate;

        // Act
        VaultFile result = await _vault.Search.Files
            .ForValueEqualTo(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchSingleAsync()
            ?? throw new Exception("File not found");

        // Assert
        result.Should().NotBeNull();
        result.ModifiedDate.Should().Be(searchValue);
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
        DateTime searchValue = file.ModifiedDate;

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueNotEqualTo(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithPaging();

        // Assert
        result.FirstOrDefault(x => x.ModifiedDate.Equals(file.ModifiedDate)).Should().BeNull();
    }

    [Fact]
    public async Task SearchFilesByDateTimeLessThan_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        DateTime searchValue = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueLessThan(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate < searchValue).Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate >= searchValue).Should().BeEmpty();
    }

    [Fact]
    public async Task SearchFilesByDateTimeLessThanOrEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        DateTime searchValue = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueLessThanOrEqualTo(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate <= searchValue).Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate > searchValue).Should().BeEmpty();
    }

    [Fact]
    public async Task SearchFilesByDateTimeGreaterThan_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        DateTime searchValue = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueGreaterThan(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate > searchValue).Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate <= searchValue).Should().BeEmpty();
    }

    [Fact]
    public async Task SearchFilesByDateTimeGreaterThanOrEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        DateTime searchValue = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueGreaterThanOrEqualTo(searchValue)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate >= searchValue).Should().NotBeEmpty();
        result.Where(x => x.ModifiedDate < searchValue).Should().BeEmpty();
    }
}

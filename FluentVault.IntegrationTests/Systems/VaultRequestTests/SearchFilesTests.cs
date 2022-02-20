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

    [Fact]
    public async Task SearchFilesWithoutPaging_ShouldReturnMoreResultsThanThePagingLimit_WhenInputsAreValid()
    {
        // This test will fail if the number of assemblies modified in the last month
        // that are still in the "In Work" state is less than the paging limit (default is 200)

        // Arrange
        string searchValue = "in work iam";
        DateTime datetime = DateTime.Now.AddMonths(-1);

        // Act
        IEnumerable<VaultFile> result = await _vault.Search.Files
            .ForValueContaining(searchValue)
            .InAllProperties
            .And
            .ForValueGreaterThan(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchWithoutPaging();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCountGreaterThan(200);
    }
}

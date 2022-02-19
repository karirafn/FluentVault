using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class SearchFilesTests
{
    private readonly VaultOptions _v;

    public SearchFilesTests()
    {
        _v = ConfigurationHelper.GetVaultOptions();
    }

    [Fact]
    public async Task SearchFilesByStringEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        string searchValue = _v.TestPartFilename;
        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        // Act
        VaultFile result = await vault.Search.Files
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
        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        // Act
        VaultFile result = await vault.Search.Files
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
        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        // Act
        IEnumerable<VaultFile> result = await vault.Search.Files
            .ForValueNotContaining(searchValue)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchAllAsync()
            ?? throw new Exception($@"Failed to search for ""{searchValue}""");

        // Assert
        _ = result.Select(x => x.MasterId.Should().NotBe(_v.TestPartMasterId));
    }

    [Fact]
    public async Task SearchFilesByDateTimeEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        VaultFile file = await vault.Search.Files
            .ForValueEqualTo(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception("File not found");
        DateTime datetime = file.ModifiedDate;

        // Act
        VaultFile result = await vault.Search.Files
            .ForValueGreaterThanOrEqualTo(datetime)
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
        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        VaultFile file = await vault.Search.Files
            .ForValueEqualTo(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync()
            ?? throw new Exception("File not found");
        DateTime datetime = file.ModifiedDate;

        // Act
        VaultFile result = await vault.Search.Files
            .ForValueGreaterThanOrEqualTo(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchSingleAsync()
            ?? throw new Exception("File not found");

        // Assert
        result.Should().NotBeNull();
        result.ModifiedDate.Should().Be(datetime);
    }

    [Fact]
    public async Task SearchFilesByDateTimeGreaterThanOrEqualTo_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        var datetime = DateTime.Now.AddMonths(-1);

        await using var vault = await Vault.SignIn
            .ToVault(_v.Server, _v.Database)
            .WithCredentials(_v.Username, _v.Password);

        // Act
        IEnumerable<VaultFile> result = await vault.Search.Files
            .ForValueGreaterThanOrEqualTo(datetime)
            .InSystemProperty(SearchDateTimeProperty.DateModified)
            .SearchAllAsync()
            ?? throw new Exception("File not found");

        // Assert
        result.Should().NotBeEmpty();
        _ = result.Select(x => x.ModifiedDate.Should().BeOnOrAfter(datetime));
    }
}

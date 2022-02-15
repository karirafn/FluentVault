using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class SearchFilesByFilenameTests
{
    [Fact]
    public async Task SearchFilesByFilename_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();

        using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        var file = await vault.Search.Files
            .ForValueContaining(v.TestPartFilename)
            .InProperty(SearchStringProperty.Filename)
            .SearchAsync();

        // Assert
        file.MasterId.Should().Be(v.TestPartMasterId);
    }
}

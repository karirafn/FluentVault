using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class SearchFilesTests
{
    [Fact]
    public async Task SearchFiles_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();

        await using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        var file = await vault.Search.Files
            .ForValueContaining(v.TestPartFilename)
            .InProperty(SearchStringProperty.Filename)
            .SearchSingleAsync();

        // Assert
        file?.MasterId.Should().Be(v.TestPartMasterId);
    }
}

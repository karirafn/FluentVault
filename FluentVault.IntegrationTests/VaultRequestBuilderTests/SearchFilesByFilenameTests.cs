using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class SearchFilesByFilenameTests : BaseTest
{
    [Fact]
    public async Task SearchFilesByFilename_ShouldReturnValidSearchResult_WhenInputsAreValid()
    {
        // Arrange
        var server = Configuration.GetValue<string>(nameof(VaultOptions.Server));
        var database = Configuration.GetValue<string>(nameof(VaultOptions.Database));
        var username = Configuration.GetValue<string>(nameof(VaultOptions.Username));
        var password = Configuration.GetValue<string>(nameof(VaultOptions.Password));
        var filename = Configuration.GetValue<string>(nameof(VaultOptions.TestPartFilename));
        var masterId = Configuration.GetValue<long>(nameof(VaultOptions.TestPartMasterId));

        using var vault = await Vault.SignIn
            .ToVault(server, database)
            .WithCredentials(username, password);

        // Act
        var file = await vault.Search.Files
            .ForValueContaining(filename)
            .InProperty(SearchStringProperty.Filename)
            .SearchAsync();

        // Assert
        file.MasterId.Should().Be(masterId);
    }
}

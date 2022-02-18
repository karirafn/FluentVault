using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetCategoriesTests
{
    [Fact]
    public async Task GetCategories_ShouldReturnCategories()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();

        await using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        var categories = await vault.Get.Categories();

        // Assert
        categories.Should().NotBeEmpty();
    }
}

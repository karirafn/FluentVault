using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetCategoriesTests : BaseRequestTest
{
    [Fact]
    public async Task GetCategories_ShouldReturnCategories()
    {
        // Arrange

        // Act
        var categories = await _vault.Get.Categories();

        // Assert
        categories.Should().NotBeEmpty();
    }
}

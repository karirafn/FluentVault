using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetLifecyclesTests : BaseRequestTest
{
    [Fact]
    public async Task GetLifecycles_ShouldReturnLifecycles()
    {
        // Arrange

        // Act
        var lifecycles = await _vault.Get.Lifecycles();

        // Assert
        lifecycles.Should().NotBeEmpty();
    }
}

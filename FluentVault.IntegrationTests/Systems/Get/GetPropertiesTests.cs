using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;

public class GetPropertiesTests : BaseRequestTest
{
    [Fact]
    public async Task GetProperties_ShouldReturnLifecycles()
    {
        // Arrange

        // Act
        IEnumerable<VaultPropertyDefinition> properties = await _vault.Get.Properties();

        // Assert
        properties.Should().NotBeEmpty();
    }
}

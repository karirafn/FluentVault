using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetPropertiesTests : BaseRequestTest
{
    [Fact]
    public async Task GetProperties_ShouldReturnLifecycles()
    {
        // Arrange

        // Act
        IEnumerable<VaultProperty> properties = await _vault.Get.Properties();

        // Assert
        properties.Should().NotBeEmpty();
    }
}

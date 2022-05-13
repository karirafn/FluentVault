using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class GetPropertyDefinitionInfosShould
{
    [Fact]
    public async Task ReturnAllLifeCycleDefinitions()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultProperty> result = await sut.Get.PropertyDefinitionInfos(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

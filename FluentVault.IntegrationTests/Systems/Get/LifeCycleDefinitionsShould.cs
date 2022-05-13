using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Get;
public class LifeCycleDefinitionsShould
{
    [Fact]
    public async Task ReturnAllLifeCycleDefinitions()
    {
        // Arrange
        VaultServiceProvider provider = new();
        IVaultClient sut = provider.GetRequiredService<IVaultClient>();

        // Act
        IEnumerable<VaultLifeCycleDefinition> result = await sut.Get.LifeCycleDefinitions(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

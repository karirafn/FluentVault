using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetLifecyclesTests
{
    [Fact]
    public async Task GetLifecycles_ShouldReturnLifecycles()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();

        await using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        var lifecycles = await vault.Get.Lifecycles();

        // Assert
        lifecycles.Should().NotBeEmpty();
    }
}

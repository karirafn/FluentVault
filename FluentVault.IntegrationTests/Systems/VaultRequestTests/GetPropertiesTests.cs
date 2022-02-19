using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class GetPropertiesTests
{
    [Fact]
    public async Task GetProperties_ShouldReturnLifecycles()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();

        await using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        IEnumerable<VaultProperty> properties = await vault.Get.Properties();

        // Assert
        properties.Should().NotBeEmpty();
    }
}

using System.Threading.Tasks;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class UpdateFileLifecycleStateTests
{
    [Fact]
    public async Task UpdateFileLifecycleStateBuilder_Should()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();
        var stateId = 5678L;

        using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        // Act
        await vault.Update.File.LifecycleState
            .WithMasterId(v.TestPartMasterId)
            .ToStateWithId(stateId)
            .WithoutComment();

        // Assert
    }
}

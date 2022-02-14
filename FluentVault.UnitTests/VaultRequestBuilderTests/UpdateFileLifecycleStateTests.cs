using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class UpdateFileLifecycleStateTests : BaseTest
{
    [Fact]
    public async Task UpdateFileLifecycleStateBuilder_Should()
    {
        // Arrange
        var server = Configuration.GetValue<string>(nameof(VaultOptions.Server));
        var database = Configuration.GetValue<string>(nameof(VaultOptions.Database));
        var username = Configuration.GetValue<string>(nameof(VaultOptions.Username));
        var password = Configuration.GetValue<string>(nameof(VaultOptions.Password));
        var masterId = Configuration.GetValue<long>(nameof(VaultOptions.TestPartMasterId));
        var stateId = 5678L;

        using var vault = await Vault.SignIn
            .ToVault(server, database)
            .WithCredentials(username, password);

        // Act
        await vault.Update.File.LifecycleState
            .WithMasterId(masterId)
            .ToStateWithId(stateId)
            .WithoutComment();

        // Assert
    }
}

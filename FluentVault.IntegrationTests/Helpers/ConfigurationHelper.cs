
using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Helpers;

public static class ConfigurationHelper
{
    public static VaultOptions GetVaultOptions()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptions>()
            .Build();

        VaultOptions vaultOptions = new()
        {
            Server = configuration.GetValue<string>(nameof(VaultOptions.Server)),
            Database = configuration.GetValue<string>(nameof(VaultOptions.Database)),
            Username = configuration.GetValue<string>(nameof(VaultOptions.Username)),
            Password = configuration.GetValue<string>(nameof(VaultOptions.Password)),
            TestPartFilename = configuration.GetValue<string>(nameof(VaultOptions.TestPartFilename)),
            TestPartMasterId = configuration.GetValue<long>(nameof(VaultOptions.TestPartMasterId)),
            TestPartDescription = configuration.GetValue<string>(nameof(VaultOptions.TestPartDescription)),
            DefaultLifecycleStateId = configuration.GetValue<long>(nameof(VaultOptions.DefaultLifecycleStateId)),
            TestingLifecycleStateId = configuration.GetValue<long>(nameof(VaultOptions.TestingLifecycleStateId))
        };

        return vaultOptions;
    }
}

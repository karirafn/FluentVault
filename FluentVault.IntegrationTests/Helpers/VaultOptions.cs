using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Helpers;

public class VaultOptions
{
    public string Server { get; init; } = string.Empty;
    public string Database { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string TestPartFilename { get; init; } = string.Empty;
    public long TestPartMasterId { get; init; }
    public string TestPartDescription { get; init; } = string.Empty;
    public long DefaultLifecycleStateId { get; init; }
    public long TestingLifecycleStateId { get; init; }

    public static VaultOptions Get()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptions>()
            .Build();

        VaultOptions vaultOptions = new()
        {
            Server = configuration.GetValue<string>(nameof(Server)),
            Database = configuration.GetValue<string>(nameof(Database)),
            Username = configuration.GetValue<string>(nameof(Username)),
            Password = configuration.GetValue<string>(nameof(Password)),
            TestPartFilename = configuration.GetValue<string>(nameof(TestPartFilename)),
            TestPartMasterId = configuration.GetValue<long>(nameof(TestPartMasterId)),
            TestPartDescription = configuration.GetValue<string>(nameof(TestPartDescription)),
            DefaultLifecycleStateId = configuration.GetValue<long>(nameof(DefaultLifecycleStateId)),
            TestingLifecycleStateId = configuration.GetValue<long>(nameof(TestingLifecycleStateId))
        };

        return vaultOptions;
    }
}


using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultOptionsFixture
{
    public VaultOptionsFixture()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptionsFixture>()
            .Build();

        Options = new()
        {
            Server = configuration.GetValue<string>(nameof(VaultOptions.Server)),
            Database = configuration.GetValue<string>(nameof(VaultOptions.Database)),
            Username = configuration.GetValue<string>(nameof(VaultOptions.Username)),
            Password = configuration.GetValue<string>(nameof(VaultOptions.Password)),
        };
    }

    public VaultOptions Options { get; }
}

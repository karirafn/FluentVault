
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultOptionsFixture
{
    private readonly IConfigurationRoot _configuration;

    public VaultOptionsFixture()
    {
        _configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptionsFixture>()
            .Build();
    }

    public IOptions<VaultOptions> Create() => Options.Create(new VaultOptions
    {
        Server = _configuration.GetValue<string>(nameof(VaultOptions.Server)),
        Database = _configuration.GetValue<string>(nameof(VaultOptions.Database)),
        Username = _configuration.GetValue<string>(nameof(VaultOptions.Username)),
        Password = _configuration.GetValue<string>(nameof(VaultOptions.Password)),
    });
}

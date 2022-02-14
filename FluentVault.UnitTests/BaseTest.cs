
using Microsoft.Extensions.Configuration;

namespace FluentVault.UnitTests;

public abstract class BaseTest
{
    protected IConfiguration Configuration { get; init; }

    public BaseTest()
    {
        Configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptions>()
            .Build();
    }
}

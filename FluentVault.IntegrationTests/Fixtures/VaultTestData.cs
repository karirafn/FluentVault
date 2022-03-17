
using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultTestData
{
    public VaultTestData()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptionsFixture>()
            .Build();

        TestPartMasterId = configuration.GetValue<long>(nameof(TestPartMasterId));
        TestPartFilename = configuration.GetValue<string>(nameof(TestPartFilename));
        TestPartDescription = configuration.GetValue<string>(nameof(TestPartDescription));
        DefaultLifecycleStateId = configuration.GetValue<long>(nameof(DefaultLifecycleStateId));
        TestingLifecycleStateId = configuration.GetValue<long>(nameof(TestingLifecycleStateId));
    }

    public long TestPartMasterId { get; set; }
    public string TestPartFilename { get; set; }
    public string TestPartDescription { get; set; }
    public long DefaultLifecycleStateId { get; set; }
    public long TestingLifecycleStateId { get; set; }
}


using Microsoft.Extensions.Configuration;

namespace FluentVault.IntegrationTests.Fixtures;
public class VaultTestData
{
    public VaultTestData()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<VaultOptionsFixture>()
            .Build();

        TestPartMasterId = new VaultMasterId(configuration.GetValue<long>(nameof(TestPartMasterId)));
        TestPartIterationId = new VaultFileIterationId(configuration.GetValue<long>(nameof(TestPartIterationId)));
        TestPartFilename = configuration.GetValue<string>(nameof(TestPartFilename));
        TestPartDescription = configuration.GetValue<string>(nameof(TestPartDescription));
        DefaultLifecycleStateId = configuration.GetValue<long>(nameof(DefaultLifecycleStateId));
        TestingLifecycleStateId = configuration.GetValue<long>(nameof(TestingLifecycleStateId));
    }

    public VaultMasterId TestPartMasterId { get; set; }
    public VaultFileIterationId TestPartIterationId { get; set; }
    public string TestPartFilename { get; set; }
    public string TestPartDescription { get; set; }
    public long DefaultLifecycleStateId { get; set; }
    public long TestingLifecycleStateId { get; set; }
}

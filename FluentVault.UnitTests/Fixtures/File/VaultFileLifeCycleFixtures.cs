
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultFileLifeCycle Group) GetVaultFileLifeCycleFixture()
    {
        Fixture fixture = new();
        VaultFileLifeCycle lifeCycle = fixture.Create<VaultFileLifeCycle>();
        string body = CreateFileLifeCycleBody(lifeCycle);

        return (body, lifeCycle);
    }

    private static string CreateFileLifeCycleBody(VaultFileLifeCycle lifeCycle)
        => $@"<FileLfCyc
LfCycStateId=""{lifeCycle.StateId}""
LfCycDefId=""{lifeCycle.DefinitionId}""
LfCycStateName=""{lifeCycle.StateName}""
Consume=""{lifeCycle.IsReleased}""
Obsolete=""{lifeCycle.IsObsolete}""/>";
}

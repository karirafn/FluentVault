
using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultLifeCycleDefinition> Files) GetVaultLifecycleFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultRestrictPurgeOption.All);
        fixture.Register(() => VaultItemToFileSecurityMode.ApplyACL);
        fixture.Register(() => VaultFolderFileSecurityMode.RemoveACL);
        fixture.Register(() => VaultBumpRevisionState.BumpProperty);
        fixture.Register(() => VaultSynchronizePropertiesState.SyncPropAndUpdateView);
        fixture.Register(() => VaultEnforceChildState.EnforceChildItemsHaveBeenReleased);
        fixture.Register(() => VaultEnforceContentState.EnforceLinkToItems);
        fixture.Register(() => VaultFileLinkTypeState.StandardComp);
        fixture.Register(() => VaultFileLinkTypeState.DesignDocs);

        return CreateBody<VaultLifeCycleDefinition>(fixture, count, "GetAllLifeCycleDefinitions", "http://AutodeskDM/Services/LifeCycle/1/7/2020/", CreateLifecycleBody);
    }

    private static string CreateLifecycleBody(VaultLifeCycleDefinition lifecycle)
        => $@"<LfCycDef 
Id=""{lifecycle.Id}""
Name=""{lifecycle.Name}""
SysName=""{lifecycle.SystemName}""
DispName=""{lifecycle.DisplayName}""
Descr=""{lifecycle.Description}""
SysAclBeh=""{lifecycle.SecurityDefinition}"">
<StateArray>
    {CreateEntityBody(lifecycle.States, CreateVaultLifeCycleStateBody)}
</StateArray>
<TransArray>
    {CreateEntityBody(lifecycle.Transitions, CreateVaultLifeCycleTransitionBody)}
</TransArray>
</LfCycDef>";
}

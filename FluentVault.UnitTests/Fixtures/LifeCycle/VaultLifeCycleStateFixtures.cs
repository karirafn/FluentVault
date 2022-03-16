
using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;
internal static partial class VaultResponseFixtures
{
    public static (string Body, VaultLifeCycleState State) GetVaultLifeCycleStateFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultRestrictPurgeOption.All);
        fixture.Register(() => VaultItemToFileSecurityMode.ApplyACL);
        fixture.Register(() => VaultFolderFileSecurityMode.RemoveACL);

        VaultLifeCycleState state = fixture.Create<VaultLifeCycleState>();
        string body = CreateVaultLifeCycleStateBody(state);

        return (body, state);
    }

    private static string CreateVaultLifeCycleStateBody(VaultLifeCycleState state)
        => $@"<State 
ID=""{state.Id}""
Name=""{state.Name}""
DispName=""{state.DisplayName}""
Descr=""{state.Description}""
IsDflt=""{state.IsDefault}""
LfCycDefId=""{state.LifecycleId}""
StateBasedSec=""{state.HasStateBasedSecurity}""
ReleasedState=""{state.IsReleasedState}""
ObsoleteState=""{state.IsObsoleteState}""
DispOrder=""{state.DisplayOrder}""
RestrictPurgeOption=""{state.RestrictPurgeOption}""
ItemFileSecMode=""{state.ItemFileSecurityMode}""
FolderFileSecMode=""{state.FolderFileSecurityMode}"">
<CommArray>
    {CreateElementArray("Comm", state.Comments)}
</CommArray>
</State>";
}

using System.Collections.Generic;

using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultLifecycle> Files) GetVaultLifecycleFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => RestrictPurgeOption.All);
        fixture.Register(() => ItemToFileSecurityMode.ApplyACL);
        fixture.Register(() => FolderFileSecurityMode.RemoveACL);
        fixture.Register(() => BumpRevisionState.BumpProperty);
        fixture.Register(() => SynchronizePropertiesState.SyncPropAndUpdateView);
        fixture.Register(() => EnforceChildState.EnforceChildItemsHaveBeenReleased);
        fixture.Register(() => EnforceContentState.EnforceLinkToItems);
        fixture.Register(() => FileLinkTypeState.StandardComp);
        fixture.Register(() => FileLinkTypeState.DesignDocs);

        return CreateBody<VaultLifecycle>(fixture, count, "GetAllLifeCycleDefinitions", "http://AutodeskDM/Services/LifeCycle/1/7/2020/", CreateLifecycleBody);
    }

    private static string CreateLifecycleBody(VaultLifecycle lifecycle) => $@"<LfCycDef
                Id=""{lifecycle.Id}""
				Name=""{lifecycle.Name}""
				SysName=""{lifecycle.SystemName}""
				DispName=""{lifecycle.DisplayName}""
				Descr=""{lifecycle.Description}""
				SysAclBeh=""{lifecycle.SecurityDefinition}"">
				<StateArray>
                    {CreateEntityBody(lifecycle.States, CreateStateBody)}
                </StateArray>
                <TransArray>
                    {CreateEntityBody(lifecycle.Transitions, CreateTransitionBody)}
                </TransArray>";

    private static string CreateStateBody(VaultLifecycleState state) => $@"<State
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
                            {CreateCommentArray(state.Comments)}
                        </CommArray>";

    private static string CreateTransitionBody(VaultLifecycleTransition transition) => $@"<Trans
                        Id=""{transition.Id}""
						FromId=""{transition.FromId}""
						ToId=""{transition.ToId}""
						Bump=""{transition.BumpRevision}""
						SyncPropOption=""{transition.SynchronizeProperties}""
						CldState=""{transition.EnforceChildState}""
						CtntState=""{transition.EnforceContentState}""
						ItemFileLnkUptodate=""{transition.ItemFileLnkUptodate}""
						ItemFileLnkState=""{transition.ItemFileLnkState}""
						CldObsState=""{transition.VerifyThatChildIsNotObsolete}""
						TransBasedSec=""{transition.TransitionBasedSecurity}""
						UpdateItems=""{transition.UpdateItems}""/>";
}

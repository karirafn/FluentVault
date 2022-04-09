
using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, VaultLifeCycleTransition Transition) GetVaultLifeCycleTransitionFixture()
    {
        Fixture fixture = new();
        fixture.Register(() => VaultBumpRevisionState.BumpProperty);
        fixture.Register(() => VaultSynchronizePropertiesState.SyncPropAndUpdatePdf);
        fixture.Register(() => VaultEnforceChildState.EnforceChildFiles);
        fixture.Register(() => VaultEnforceContentState.EnforceLinkToFiles);
        fixture.Register(() => VaultFileLinkTypeState.DesignDocs);

        VaultLifeCycleTransition transition = fixture.Create<VaultLifeCycleTransition>();
        string body = CreateVaultLifeCycleTransitionBody(transition);

        return (body, transition);
    }

    private static string CreateVaultLifeCycleTransitionBody(VaultLifeCycleTransition transition)
        => $@"<Trans 
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

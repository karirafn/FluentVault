using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultLifeCycleTransitionSerializer : XElementSerializer<VaultLifeCycleTransition>
{
    private const string Trans = nameof(Trans);
    private const string Bump = nameof(Bump);
    private const string SyncPropOption = nameof(SyncPropOption);
    private const string CldState = nameof(CldState);
    private const string CtntState = nameof(CtntState);
    private const string ItemFileLnkUptodate = nameof(ItemFileLnkUptodate);
    private const string ItemFileLnkState = nameof(ItemFileLnkState);
    private const string CldObsState = nameof(CldObsState);
    private const string TransBasedSec = nameof(TransBasedSec);

    public VaultLifeCycleTransitionSerializer(XNamespace @namespace) : base(Trans, @namespace) { }

    internal override VaultLifeCycleTransition Deserialize(XElement element)
        => new(
            element.ParseAttributeValue(nameof(VaultLifeCycleTransition.Id), VaultLifeCycleTransitionId.Parse),
            element.ParseAttributeValue(nameof(VaultLifeCycleTransition.FromId), VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue(nameof(VaultLifeCycleTransition.ToId), VaultLifeCycleStateId.Parse),
            element.ParseAttributeValue(Bump, x => VaultBumpRevisionState.FromName(x)),
            element.ParseAttributeValue(SyncPropOption, x => VaultSynchronizePropertiesState.FromName(x)),
            element.ParseAttributeValue(CldState, x => VaultEnforceChildState.FromName(x)),
            element.ParseAttributeValue(CtntState, x => VaultEnforceContentState.FromName(x)),
            element.ParseAttributeValue(ItemFileLnkUptodate, x => VaultFileLinkTypeState.FromName(x)),
            element.ParseAttributeValue(ItemFileLnkState, x => VaultFileLinkTypeState.FromName(x)),
            element.ParseAttributeValue(CldObsState, bool.Parse),
            element.ParseAttributeValue(TransBasedSec, bool.Parse),
            element.ParseAttributeValue(nameof(VaultLifeCycleTransition.UpdateItems), bool.Parse));

    internal override XElement Serialize(VaultLifeCycleTransition transition)
        => BaseElement
            .AddAttribute(nameof(VaultLifeCycleTransition.Id), transition.Id)
            .AddAttribute(nameof(VaultLifeCycleTransition.FromId), transition.FromId)
            .AddAttribute(nameof(VaultLifeCycleTransition.ToId), transition.ToId)
            .AddAttribute(Bump, transition.BumpRevision)
            .AddAttribute(SyncPropOption, transition.SynchronizeProperties)
            .AddAttribute(CldState, transition.EnforceChildState)
            .AddAttribute(CtntState, transition.EnforceContentState)
            .AddAttribute(ItemFileLnkUptodate, transition.ItemFileLnkUptodate)
            .AddAttribute(ItemFileLnkState, transition.ItemFileLnkState)
            .AddAttribute(CldObsState, transition.VerifyThatChildIsNotObsolete)
            .AddAttribute(TransBasedSec, transition.TransitionBasedSecurity)
            .AddAttribute(nameof(VaultLifeCycleTransition.UpdateItems), transition.UpdateItems);
}

using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultLifeCycleStateSerializer : XElementSerializer<VaultLifeCycleState>
{
    private const string State = nameof(State);
    private const string DispName = nameof(DispName);
    private const string Descr = nameof(Descr);
    private const string IsDflt = nameof(IsDflt);
    private const string LfCycDefId = nameof(LfCycDefId);
    private const string StateBasedSec = nameof(StateBasedSec);
    private const string ReleasedState = nameof(ReleasedState);
    private const string ObsoleteState = nameof(ObsoleteState);
    private const string DispOrder = nameof(DispOrder);
    private const string ItemFileSecMode = nameof(ItemFileSecMode);
    private const string FolderFileSecMode = nameof(FolderFileSecMode);
    private const string CommArray = nameof(CommArray);
    private const string Comm = nameof(Comm);

    public VaultLifeCycleStateSerializer(XNamespace @namespace) : base(State, @namespace) { }

    internal override VaultLifeCycleState Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultLifeCycleState.Id).ToUpper(), VaultLifeCycleStateId.Parse),
            element.GetAttributeValue(nameof(VaultLifeCycleState.Name)),
            element.GetAttributeValue(DispName),
            element.GetAttributeValue(Descr),
            element.ParseAttributeValue(IsDflt, bool.Parse),
            element.ParseAttributeValue(LfCycDefId, VaultLifeCycleDefinitionId.Parse),
            element.ParseAttributeValue(StateBasedSec, bool.Parse),
            element.ParseAttributeValue(ReleasedState, bool.Parse),
            element.ParseAttributeValue(ObsoleteState, bool.Parse),
            element.ParseAttributeValue(DispOrder, long.Parse),
            element.ParseAttributeValue(nameof(VaultLifeCycleState.RestrictPurgeOption), x => VaultRestrictPurgeOption.FromName(x)),
            element.ParseAttributeValue(ItemFileSecMode, x => VaultItemToFileSecurityMode.FromName(x)),
            element.ParseAttributeValue(FolderFileSecMode, x => VaultFolderFileSecurityMode.FromName(x)),
            element.ParseAllElements(Comm, x => x.Value));

    internal override XElement Serialize(VaultLifeCycleState state)
        => BaseElement.AddAttribute(nameof(VaultLifeCycleState.Id).ToUpper(), state.Id)
            .AddAttribute(nameof(VaultLifeCycleState.Name), state.Name)
            .AddAttribute(DispName, state.DisplayName)
            .AddAttribute(Descr, state.Description)
            .AddAttribute(IsDflt, state.IsDefault)
            .AddAttribute(LfCycDefId, state.LifecycleId)
            .AddAttribute(StateBasedSec, state.HasStateBasedSecurity)
            .AddAttribute(ReleasedState, state.IsReleasedState)
            .AddAttribute(ObsoleteState, state.IsObsoleteState)
            .AddAttribute(DispOrder, state.DisplayOrder)
            .AddAttribute(nameof(VaultLifeCycleState.RestrictPurgeOption), state.RestrictPurgeOption)
            .AddAttribute(ItemFileSecMode, state.ItemFileSecurityMode)
            .AddAttribute(FolderFileSecMode, state.FolderFileSecurityMode)
            .AddNestedElements(Namespace, CommArray, Comm, state.Comments);
}

using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultLifeCycleDefinitionSerializer : XElementSerializer<VaultLifeCycleDefinition>
{
    private const string LfCycDef = nameof(LfCycDef);
    private const string SysName = nameof(SysName);
    private const string DispName = nameof(DispName);
    private const string Descr = nameof(Descr);
    private const string SysAclBeh = nameof(SysAclBeh);
    private const string StateArray = nameof(StateArray);
    private const string TransArray = nameof(TransArray);

    private readonly VaultLifeCycleStateSerializer _stateSerializer;
    private readonly VaultLifeCycleTransitionSerializer _transitionSerializer;

    public VaultLifeCycleDefinitionSerializer(XNamespace @namespace) : base(LfCycDef, @namespace)
    {
        _stateSerializer = new(Namespace);
        _transitionSerializer = new(Namespace);
    }

    internal override VaultLifeCycleDefinition Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultLifeCycleDefinition.Id), long.Parse),
            element.GetAttributeValue(nameof(VaultLifeCycleDefinition.Name)),
            element.GetAttributeValue(SysName),
            element.GetAttributeValue(DispName),
            element.GetAttributeValue(Descr),
            element.GetAttributeValue(SysAclBeh),
            _stateSerializer.DeserializeMany(element),
            _transitionSerializer.DeserializeMany(element));

    internal override XElement Serialize(VaultLifeCycleDefinition lifeCycle)
        => BaseElement
            .AddAttribute(nameof(VaultLifeCycleDefinition.Id), lifeCycle.Id)
            .AddAttribute(nameof(VaultLifeCycleDefinition.Name), lifeCycle.Name)
            .AddAttribute(SysName, lifeCycle.SystemName)
            .AddAttribute(DispName, lifeCycle.DisplayName)
            .AddAttribute(Descr, lifeCycle.Description)
            .AddAttribute(SysAclBeh, lifeCycle.SecurityDefinition)
            .AddElement(_stateSerializer.SerializeMany(StateArray, lifeCycle.States))
            .AddElement(_transitionSerializer.SerializeMany(TransArray, lifeCycle.Transitions));
}

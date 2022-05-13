using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault.Domain.Entity;
internal class VaultEntityLifeCycleSerializer : XElementSerializer<VaultEntityLifeCycle>
{
    private const string LfCyc = nameof(LfCyc);
    private const string LfCycStateId = nameof(LfCycStateId);
    private const string LfCycDefId = nameof(LfCycDefId);
    private const string Consume = nameof(Consume);
    private const string Obsolete = nameof(Obsolete);

    public VaultEntityLifeCycleSerializer(XNamespace @namespace) : base(LfCyc, @namespace) { }

    internal override VaultEntityLifeCycle Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(LfCycStateId, VaultLifeCycleStateId.Parse),
                   element.ParseAttributeValue(LfCycDefId, VaultLifeCycleDefinitionId.Parse),
                   element.ParseAttributeValue(Consume, bool.Parse),
                   element.ParseAttributeValue(Obsolete, bool.Parse));
    }

    internal override XElement Serialize(VaultEntityLifeCycle lifeCycle)
        => BaseElement
            .AddAttribute(LfCycStateId, lifeCycle.StateId)
            .AddAttribute(LfCycDefId, lifeCycle.DefinitionId)
            .AddAttribute(Consume, lifeCycle.IsReleased)
            .AddAttribute(Obsolete, lifeCycle.IsObsolete);
}

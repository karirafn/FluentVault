using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultFileRevisionSerializer : XElementSerializer<VaultFileRevision>
{
    public const string FileRev = nameof(FileRev);
    public const string RevId = nameof(RevId);
    public const string RevDefId = nameof(RevDefId);
    public const string Label = nameof(Label);
    public const string MaxConsumeFileId = nameof(MaxConsumeFileId);
    public const string MaxFileId = nameof(MaxFileId);
    public const string MaxRevId = nameof(MaxRevId);
    public const string Order = nameof(Order);

    public VaultFileRevisionSerializer(XNamespace @namespace) : base(FileRev, @namespace) { }

    internal override VaultFileRevision Deserialize(XElement element)
    {
        element = GetSerializationElement(element);

        return new(element.ParseAttributeValue(RevId, VaultRevisionId.Parse),
                   element.ParseAttributeValue(RevDefId, VaultRevisionDefinitionId.Parse),
                   element.GetAttributeValue(Label),
                   element.ParseAttributeValue(MaxConsumeFileId, long.Parse),
                   element.ParseAttributeValue(MaxFileId, long.Parse),
                   element.ParseAttributeValue(MaxRevId, long.Parse),
                   element.ParseAttributeValue(Order, long.Parse));
    }

    internal override XElement Serialize(VaultFileRevision revision)
        => BaseElement
            .AddAttribute(RevId, revision.Id)
            .AddAttribute(RevDefId, revision.DefinitionId)
            .AddAttribute(Label, revision.Label)
            .AddAttribute(MaxConsumeFileId, revision.MaximumConsumeFileId)
            .AddAttribute(MaxFileId, revision.MaximumFileId)
            .AddAttribute(MaxRevId, revision.MaximumRevisionId)
            .AddAttribute(Order, revision.Order);
}

using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileRevision(
    RevisionId Id,
    RevisionDefinitionId DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order)
{
    internal static VaultFileRevision Parse(XElement element)
        => new(RevisionId.ParseFromAttribute(element, "RevId"),
            RevisionDefinitionId.ParseFromAttribute(element, "RevDefId"),
            element.GetAttributeValue("Label"),
            element.ParseAttributeValue("MaxConsumeFileId", long.Parse),
            element.ParseAttributeValue("MaxFileId", long.Parse),
            element.ParseAttributeValue("MaxRevId", long.Parse),
            element.ParseAttributeValue("Order", long.Parse));
}

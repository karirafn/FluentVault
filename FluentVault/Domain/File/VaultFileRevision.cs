using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileRevision(
    RevisionId Id,
    long DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order)
{
    internal static VaultFileRevision Parse(XElement element)
        => new(RevisionId.ParseFromAttribute(element, "RevId"),
            element.ParseAttributeValue("RevDefId", long.Parse),
            element.GetAttributeValue("Label"),
            element.ParseAttributeValue("MaxConsumeFileId", long.Parse),
            element.ParseAttributeValue("MaxFileId", long.Parse),
            element.ParseAttributeValue("MaxRevId", long.Parse),
            element.ParseAttributeValue("Order", long.Parse));
}

using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFileRevision(
    VaultRevisionId Id,
    VaultRevisionDefinitionId DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order)
{
    internal static VaultFileRevision Parse(XElement element)
        => new(element.ParseAttributeValue("RevId", VaultRevisionId.Parse),
            element.ParseAttributeValue("RevDefId", VaultRevisionDefinitionId.Parse),
            element.GetAttributeValue("Label"),
            element.ParseAttributeValue("MaxConsumeFileId", long.Parse),
            element.ParseAttributeValue("MaxFileId", long.Parse),
            element.ParseAttributeValue("MaxRevId", long.Parse),
            element.ParseAttributeValue("Order", long.Parse));
}

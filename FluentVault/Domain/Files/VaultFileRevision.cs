using System.Xml.Linq;

using FluentVault.Common.Extensions;

namespace FluentVault;

public record VaultFileRevision(
    long Id,
    long DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order)
{
    internal static VaultFileRevision Parse(XElement element)
        => new(element.ParseAttributeValue("RevId", long.Parse),
            element.ParseAttributeValue("RevDefId", long.Parse),
            element.GetAttributeValue("Label"),
            element.ParseAttributeValue("MaxConsumeFileId", long.Parse),
            element.ParseAttributeValue("MaxFileId", long.Parse),
            element.ParseAttributeValue("MaxRevId", long.Parse),
            element.ParseAttributeValue("Order", long.Parse));
}

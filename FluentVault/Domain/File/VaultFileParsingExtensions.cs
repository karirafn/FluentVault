using System.Xml.Linq;

namespace FluentVault;

internal static class VaultFileParsingExtensions
{
    private const string FileElementName = "File";

    internal static VaultFile ParseVaultFile(this XDocument document)
        => document.ParseSingleElement(FileElementName, ParseVaultFile);

    internal static IEnumerable<VaultFile> ParseAllVaultFiles(this XDocument document)
        => document.ParseAllElements(FileElementName, ParseVaultFile);

    private static VaultFile ParseVaultFile(this XElement element)
        => new(element.ParseAttributeValue("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.ParseAttributeValue("MasterId", long.Parse),
            element.GetAttributeValue("VerName"),
            element.ParseAttributeValue("VerNum", long.Parse),
            element.ParseAttributeValue("MaxCkInVerNum", long.Parse),
            element.GetAttributeValue("Comm"),
            element.ParseAttributeValue("CkInDate", DateTime.Parse),
            element.ParseAttributeValue("CreateDate", DateTime.Parse),
            element.ParseAttributeValue("ModDate", DateTime.Parse),
            element.ParseAttributeValue("CreateUserId", long.Parse),
            element.GetAttributeValue("CreateUserName"),
            element.ParseAttributeValue("Cksum", long.Parse),
            element.ParseAttributeValue("FileSize", long.Parse),
            element.ParseAttributeValue("CheckedOut", bool.Parse),
            element.ParseAttributeValue("FolderId", long.Parse),
            element.GetAttributeValue("CkOutSpec"),
            element.GetAttributeValue("CkOutMach"),
            element.ParseAttributeValue("CkOutUserId", long.Parse),
            element.GetAttributeValue("FileClass"),
            element.ParseAttributeValue("FileStatus", VaultFileStatus.Parse),
            element.ParseAttributeValue("Locked", bool.Parse),
            element.ParseAttributeValue("Hidden", bool.Parse),
            element.ParseAttributeValue("Cloaked", bool.Parse),
            element.ParseAttributeValue("IsOnSite", bool.Parse),
            element.ParseAttributeValue("ControlledByChangeOrder", bool.Parse),
            element.GetAttributeValue("DesignVisAttmtStatus"),
            element.ParseSingleElement("FileRev", ParseRevision),
            element.ParseSingleElement("FileLfCyc", ParseLifecycle),
            element.ParseSingleElement("Cat", ParseCategory));

    private static VaultFileRevision ParseRevision(XElement element)
        => new(element.ParseAttributeValue("RevId", long.Parse),
            element.ParseAttributeValue("RevDefId", long.Parse),
            element.GetAttributeValue("Label"),
            element.ParseAttributeValue("MaxConsumeFileId", long.Parse),
            element.ParseAttributeValue("MaxFileId", long.Parse),
            element.ParseAttributeValue("MaxRevId", long.Parse),
            element.ParseAttributeValue("Order", long.Parse));

    private static VaultFileLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeValue("LfCycStateId", long.Parse),
            element.ParseAttributeValue("LfCycDefId", long.Parse),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeValue("Consume", bool.Parse),
            element.ParseAttributeValue("Obsolete", bool.Parse));

    private static VaultFileCategory ParseCategory(XElement element)
        => new(element.ParseAttributeValue("CatId", long.Parse),
            element.GetAttributeValue("CatName"));
}

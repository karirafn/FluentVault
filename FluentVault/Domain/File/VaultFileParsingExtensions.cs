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
        => new(element.ParseAttribute("Id", long.Parse),
            element.GetAttributeValue("Name"),
            element.ParseAttribute("MasterId", long.Parse),
            element.GetAttributeValue("VerName"),
            element.ParseAttribute("VerNum", long.Parse),
            element.ParseAttribute("MaxCkInVerNum", long.Parse),
            element.GetAttributeValue("Comm"),
            element.ParseAttribute("CkInDate", DateTime.Parse),
            element.ParseAttribute("CreateDate", DateTime.Parse),
            element.ParseAttribute("ModDate", DateTime.Parse),
            element.ParseAttribute("CreateUserId", long.Parse),
            element.GetAttributeValue("CreateUserName"),
            element.ParseAttribute("Cksum", long.Parse),
            element.ParseAttribute("FileSize", long.Parse),
            element.ParseAttribute("CheckedOut", bool.Parse),
            element.ParseAttribute("FolderId", long.Parse),
            element.GetAttributeValue("CkOutSpec"),
            element.GetAttributeValue("CkOutMach"),
            element.ParseAttribute("CkOutUserId", long.Parse),
            element.GetAttributeValue("FileClass"),
            element.ParseAttribute("FileStatus", VaultFileStatus.Parse),
            element.ParseAttribute("Locked", bool.Parse),
            element.ParseAttribute("Hidden", bool.Parse),
            element.ParseAttribute("Cloaked", bool.Parse),
            element.ParseAttribute("IsOnSite", bool.Parse),
            element.ParseAttribute("ControlledByChangeOrder", bool.Parse),
            element.GetAttributeValue("DesignVisAttmtStatus"),
            element.ParseSingleElement("FileRev", ParseRevision),
            element.ParseSingleElement("FileLfCyc", ParseLifecycle),
            element.ParseSingleElement("Cat", ParseCategory));

    private static VaultFileRevision ParseRevision(XElement element)
        => new(element.ParseAttribute("RevId", long.Parse),
            element.ParseAttribute("RevDefId", long.Parse),
            element.GetAttributeValue("Label"),
            element.ParseAttribute("MaxConsumeFileId", long.Parse),
            element.ParseAttribute("MaxFileId", long.Parse),
            element.ParseAttribute("MaxRevId", long.Parse),
            element.ParseAttribute("Order", long.Parse));

    private static VaultFileLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttribute("LfCycStateId", long.Parse),
            element.ParseAttribute("LfCycDefId", long.Parse),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttribute("Consume", bool.Parse),
            element.ParseAttribute("Obsolete", bool.Parse));

    private static VaultCategory ParseCategory(XElement element)
        => new(element.ParseAttribute("CatId", long.Parse),
            element.GetAttributeValue("CatName"));
}

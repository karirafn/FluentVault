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
        => new(element.ParseAttributeAsLong("Id"),
            element.GetAttributeValue("Name"),
            element.ParseAttributeAsLong("MasterId"),
            element.GetAttributeValue("VerName"),
            element.ParseAttributeAsLong("VerNum"),
            element.ParseAttributeAsLong("MaxCkInVerNum"),
            element.GetAttributeValue("Comm"),
            element.ParseAttributeAsDateTime("CkInDate"),
            element.ParseAttributeAsDateTime("CreateDate"),
            element.ParseAttributeAsDateTime("ModDate"),
            element.ParseAttributeAsLong("CreateUserId"),
            element.GetAttributeValue("CreateUserName"),
            element.ParseAttributeAsLong("Cksum"),
            element.ParseAttributeAsLong("FileSize"),
            element.ParseAttributeAsBool("CheckedOut"),
            element.ParseAttributeAsLong("FolderId"),
            element.GetAttributeValue("CkOutSpec"),
            element.GetAttributeValue("CkOutMach"),
            element.ParseAttributeAsLong("CkOutUserId"),
            element.GetAttributeValue("FileClass"),
            element.GetAttributeValue("FileStatus"),
            element.ParseAttributeAsBool("Locked"),
            element.ParseAttributeAsBool("Hidden"),
            element.ParseAttributeAsBool("Cloaked"),
            element.ParseAttributeAsBool("IsOnSite"),
            element.ParseAttributeAsBool("ControlledByChangeOrder"),
            element.GetAttributeValue("DesignVisAttmtStatus"),
            element.ParseSingleElement("FileRev", ParseRevision),
            element.ParseSingleElement("FileLfCyc", ParseLifecycle),
            element.ParseSingleElement("Cat", ParseCategory));

    private static VaultFileRevision ParseRevision(XElement element)
        => new(element.ParseAttributeAsLong("RevId"),
            element.ParseAttributeAsLong("RevDefId"),
            element.GetAttributeValue("Label"),
            element.ParseAttributeAsLong("MaxConsumeFileId"),
            element.ParseAttributeAsLong("MaxFileId"),
            element.ParseAttributeAsLong("MaxRevId"),
            element.ParseAttributeAsLong("Order"));

    private static VaultFileLifecycle ParseLifecycle(XElement element)
        => new(element.ParseAttributeAsLong("LfCycStateId"),
            element.ParseAttributeAsLong("LfCycDefId"),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeAsBool("Consume"),
            element.ParseAttributeAsBool("Obsolete"));

    private static VaultCategory ParseCategory(XElement element)
        => new(element.ParseAttributeAsLong("CatId"),
            element.GetAttributeValue("CatName"));
}

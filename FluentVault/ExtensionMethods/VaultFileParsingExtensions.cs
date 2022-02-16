using System.Xml.Linq;

namespace FluentVault;

internal static class VaultFileParsingExtensions
{
    internal static VaultFile ParseVaultFile(this XDocument document)
        => document.GetElementByName("File")
            .ParseVaultFile();

    internal static IEnumerable<VaultFile> ParseAllVaultFiles(this XDocument document)
        => document.Descendants()
            .Where(x => x.Name.LocalName.Equals("File"))
            .Select(x => ParseVaultFile(x));

    private static VaultFile ParseVaultFile(this XElement element)
    {
        long id = element.ParseAttributeAsLong("Id");
        long masterId = element.ParseAttributeAsLong("MasterId");
        long versionNumber = element.ParseAttributeAsLong("VerNum");
        long maximumCheckInVersionNumber = element.ParseAttributeAsLong("MaxCkInVerNum");
        long createUserId = element.ParseAttributeAsLong("CreateUserId");
        long checksum = element.ParseAttributeAsLong("Cksum");
        long filesize = element.ParseAttributeAsLong("FileSize");
        long folderId = element.ParseAttributeAsLong("FolderId");
        long checkedOutUserId = element.ParseAttributeAsLong("CkOutUserId");

        bool isCheckedOut = element.ParseAttributeAsBool("CheckedOut");
        bool isLocked = element.ParseAttributeAsBool("Locked");
        bool isHidden = element.ParseAttributeAsBool("Hidden");
        bool isCloaked = element.ParseAttributeAsBool("Cloaked");
        bool isOnSite = element.ParseAttributeAsBool("IsOnSite");
        bool isControlledByChangeOrder = element.ParseAttributeAsBool("ControlledByChangeOrder");

        DateTime checkedInDate = element.ParseAttributeAsDateTime("CkInDate");
        DateTime createDate = element.ParseAttributeAsDateTime("CreateDate");
        DateTime modifiedDate = element.ParseAttributeAsDateTime("ModDate");

        string filename = element.GetAttributeValue("Name");
        string versionName = element.GetAttributeValue("VerName");
        string comment = element.GetAttributeValue("Comm");
        string createUsername = element.GetAttributeValue("CreateUserName");
        string checkedOutPath = element.GetAttributeValue("CkOutSpec");
        string checkedOutMachine = element.GetAttributeValue("CkOutMach");
        string fileClass = element.GetAttributeValue("FileClass");
        string fileStatus = element.GetAttributeValue("FileStatus");
        string designVisualAttachmentStatus = element.GetAttributeValue("DesignVisAttmtStatus");

        VaultFileRevision revision = element
            .GetElementByName("FileRev")?
            .ParseRevision()
            ?? throw new Exception("Failed to parse revision");

        VaultFileLifecycle lifecycle = element
            .GetElementByName("FileLfCyc")?
            .ParseLifecycle()
            ?? throw new Exception("Failed to parse lifecycle");

        VaultCategory category = element
            .GetElementByName("Cat")?
            .ParseCategory()
            ?? throw new Exception("Failed to parse category");

        return new VaultFile(
            id,
            filename,
            masterId,
            versionName,
            versionNumber,
            maximumCheckInVersionNumber,
            comment,
            checkedInDate,
            createDate,
            modifiedDate,
            createUserId,
            createUsername,
            checksum,
            filesize,
            isCheckedOut,
            folderId,
            checkedOutPath,
            checkedOutMachine,
            checkedOutUserId,
            fileClass,
            fileStatus,
            isLocked,
            isHidden,
            isCloaked,
            isOnSite,
            isControlledByChangeOrder,
            designVisualAttachmentStatus,
            revision,
            lifecycle,
            category);
    }

    private static VaultFileRevision ParseRevision(this XElement element)
        => new(element.ParseAttributeAsLong("RevId"),
            element.ParseAttributeAsLong("RevDefId"),
            element.GetAttributeValue("Label"),
            element.ParseAttributeAsLong("MaxConsumeFileId"),
            element.ParseAttributeAsLong("MaxFileId"),
            element.ParseAttributeAsLong("MaxRevId"),
            element.ParseAttributeAsLong("Order"));

    private static VaultFileLifecycle ParseLifecycle(this XElement element)
        => new(element.ParseAttributeAsLong("LfCycStateId"),
            element.ParseAttributeAsLong("LfCycDefId"),
            element.GetAttributeValue("LfCycStateName"),
            element.ParseAttributeAsBool("Consume"),
            element.ParseAttributeAsBool("Obsolete"));

    private static VaultCategory ParseCategory(this XElement element)
        => new(element.ParseAttributeAsLong("CatId"),
            element.GetAttributeValue("CatName"));
}

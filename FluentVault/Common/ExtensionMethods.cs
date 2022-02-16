using System.Xml.Linq;

namespace FluentVault;

internal static class ExtensionMethods
{
    internal static VaultHttpRequestMessage CreateVaultHttpRequestMessage(this VaultStringContent content, Uri uri, string soapAction)
        => new(uri, content, soapAction);

    internal static string GetElementValue(this XDocument document, string name)
        => document.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals(name))?
            .Value
        ?? throw new KeyNotFoundException($"Failed to get element with name {name}.");

    internal static (string, string) GetElementValues(this XDocument document, string a, string b)
        => (document.GetElementValue(a), document.GetElementValue(b));

    internal static VaultFile ParseVaultFile(this XDocument document)
        => document.GetElementByName("File")
            .ParseVaultFile();

    internal static IEnumerable<VaultFile> ParseAllVaultFiles(this XDocument document)
        => document.Descendants()
            .Where(x => x.Name.LocalName.Equals("File"))
            .Select(x => ParseVaultFile(x));

    private static XElement GetElementByName(this XDocument document, string name)
        => document.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($"Faield to get element with name {name}.");

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

    private static string GetAttributeValue(this XElement element, string name)
        => element.Attribute(name)?.Value
        ?? throw new KeyNotFoundException($"Attribute {name} was not found.");

    private static XElement GetElementByName(this XElement element, string name)
        => element.Descendants().FirstOrDefault(x => x.Name.LocalName.Equals(name))
        ?? throw new KeyNotFoundException($"Nested element {name} was not found in element {element.Name}");

    private static long ParseAttributeAsLong(this XElement element, string name)
        => long.TryParse(element.GetAttributeValue(name), out long value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type long");

    private static bool ParseAttributeAsBool(this XElement element, string name)
        => bool.TryParse(element.GetAttributeValue(name), out bool value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type bool");

    private static DateTime ParseAttributeAsDateTime(this XElement element, string name)
        => DateTime.TryParse(element.GetAttributeValue(name), out DateTime value)
        ? value
        : throw new ArgumentException($@"Failed to parse attribute ""{name}"" as type DateTime");
}

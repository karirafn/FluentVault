using System.Xml.Linq;

namespace FluentVault;

internal static class ExtensionMethods
{
    internal static VaultHttpRequestMessage CreateVaultHttpRequestMessage(this VaultStringContent content, Uri uri, string soapAction)
        => new(uri, content, soapAction);

    internal static string? GetElementValue(this XDocument? document, string name)
        => document?.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals(name))?
            .Value ?? throw new Exception($"Element {name} was not found");

    internal static (string?, string?) GetElementValues(this XDocument? document, string a, string b)
        => (document?.GetElementValue(a), document?.GetElementValue(b));

    internal static IEnumerable<VaultFile> ParseAllFileSearchResults(this XDocument document)
        => document.Descendants()
            .Where(x => x.Name.LocalName.Equals("File"))
            .Select(x => ParseVaultFile(x));

    internal static VaultFile ParseVaultFile(this XDocument document)
        => ParseVaultFile(document?.Descendants()
            .FirstOrDefault(x => x.Name.LocalName.Equals("File")));

    private static VaultFile ParseVaultFile(this XElement? element)
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

        string filename = element?.Attribute("Name")?.Value ?? string.Empty;
        string versionName = element?.Attribute("VerName")?.Value ?? string.Empty;
        string comment = element?.Attribute("Comm")?.Value ?? string.Empty;
        string createUsername = element?.Attribute("CreateUserName")?.Value ?? string.Empty;
        string checkedOutPath = element?.Attribute("CkOutSpec")?.Value ?? string.Empty;
        string checkedOutMachine = element?.Attribute("CkOutMach")?.Value ?? string.Empty;
        string fileClass = element?.Attribute("FileClass")?.Value ?? string.Empty;
        string fileStatus = element?.Attribute("FileStatus")?.Value ?? string.Empty;
        string designVisualAttachmentStatus = element?.Attribute("DesignVisAttmtStatus")?.Value ?? string.Empty;

        VaultFileRevision? revision = element?.GetElementByName("FileRev").ParseRevision();
        VaultFileLifecycle? lifecycle = element?.GetElementByName("FileLfCyc").ParseLifecycle();
        VaultCategory? category = element?.GetElementByName("Cat").ParseCategory();

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

    private static VaultFileRevision ParseRevision(this XElement? element)
    {
        long id = element.ParseAttributeAsLong("RevId");
        long maximumConsumeFileId = element.ParseAttributeAsLong("MaxConsumeFileId");
        long maximumFileId = element.ParseAttributeAsLong("MaxFileId");
        long definitionId = element.ParseAttributeAsLong("RevDefId");
        long maximumRevisionId = element.ParseAttributeAsLong("MaxRevId");
        long order = element.ParseAttributeAsLong("Order");

        string label = element?.Attribute("Label")?.Value ?? string.Empty;

        return new(id, definitionId, label, maximumConsumeFileId, maximumFileId, maximumRevisionId, order);
    }

    private static VaultFileLifecycle ParseLifecycle(this XElement? element)
    {
        long stateId = element.ParseAttributeAsLong("LfCycStateId");
        long definitonId = element.ParseAttributeAsLong("LfCycDefId");

        bool isConsume = element.ParseAttributeAsBool("Consume");
        bool isObsolete = element.ParseAttributeAsBool("Obsolete");

        string stateName = element?.Attribute("LfCycStateName")?.Value ?? string.Empty;

        return new(stateId, definitonId, stateName, isConsume, isObsolete);
    }

    private static VaultCategory ParseCategory(this XElement? element)
    {
        long id = element.ParseAttributeAsLong("CatId");

        string name = element?.Attribute("CatName")?.Value ?? string.Empty;

        return new(id, name);
    }

    private static XElement? GetElementByName(this XElement? element, string name)
        => element?.Descendants().FirstOrDefault(x => x.Name.LocalName.Equals(name));

    private static long ParseAttributeAsLong(this XElement? element, string name)
    {
        if (long.TryParse(element?.Attribute(name)?.Value, out long value) is false)
            throw new ArgumentException($"Failed to parse {name}");
        return value;
    }

    private static bool ParseAttributeAsBool(this XElement? element, string name)
    {
        if (bool.TryParse(element?.Attribute(name)?.Value, out bool value) is false)
            throw new ArgumentException($"Failed to parse {name}");
        return value;
    }

    private static DateTime ParseAttributeAsDateTime(this XElement? element, string name)
    {
        if (DateTime.TryParse(element?.Attribute(name)?.Value, out DateTime value) is false)
            throw new ArgumentException($"Failed to parse {name}");
        return value;
    }
}

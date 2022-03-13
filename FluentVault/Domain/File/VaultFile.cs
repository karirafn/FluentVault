using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;

public record VaultFile(
    FileId Id,
    string Filename,
    long MasterId,
    string VersionName,
    long VersionNumber,
    long MaximumCheckInVersionNumber,
    string Comment,
    DateTime CheckedInDate,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    long CreateUserId,
    string CreateUserName,
    long CheckSum,
    long FileSize,
    bool IsCheckedOut,
    long FolderId,
    string CheckedOutPath,
    string CheckedOutMachine,
    long CheckedOutUserId,
    string FileClass,
    VaultFileStatus FileStatus,
    bool IsLocked,
    bool IsHidden,
    bool IsCloaked,
    bool IsOnSite,
    bool IsControlledByChangeOrder,
    string DesignVisualAttachmentStatus,
    VaultFileRevision Revision,
    VaultFileLifeCycle Lifecycle,
    VaultFileCategory Category)
{
    private const string FileElementName = "File";

    internal static VaultFile ParseSingle(XDocument document)
        => document.ParseElement(FileElementName, ParseVaultFile);

    internal static IEnumerable<VaultFile> ParseAll(XDocument document)
        => document.ParseAllElements(FileElementName, ParseVaultFile);

    private static VaultFile ParseVaultFile(XElement element)
        => new(FileId.ParseFromAttribute(element),
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
            element.ParseAttributeValue("FileStatus", x => VaultFileStatus.FromName(x)),
            element.ParseAttributeValue("Locked", bool.Parse),
            element.ParseAttributeValue("Hidden", bool.Parse),
            element.ParseAttributeValue("Cloaked", bool.Parse),
            element.ParseAttributeValue("IsOnSite", bool.Parse),
            element.ParseAttributeValue("ControlledByChangeOrder", bool.Parse),
            element.GetAttributeValue("DesignVisAttmtStatus"),
            element.ParseElement("FileRev", VaultFileRevision.Parse),
            element.ParseElement("FileLfCyc", VaultFileLifeCycle.Parse),
            element.ParseElement("Cat", VaultFileCategory.Parse));
}

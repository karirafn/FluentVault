using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault;
public record VaultFolder(
    VaultFolderId Id,
    string Name,
    string Path,
    VaultFolderId ParentFolderId,
    DateTime CreateDate,
    string CreateUserName,
    VaultUserId CreateUserId,
    int ChildFolderCount,
    bool IsLibraryFolder,
    bool IsCloaked,
    bool IsLocked,
    VaultEntityCategory Category)
{
    private const string Folder = nameof(Folder);

    internal static VaultFolder DeserializeSingle(XDocument document)
        => document.ParseElement(Folder, Deserialize);

    internal static IEnumerable<VaultFolder> DeserializeAll(XDocument document)
        => document.ParseAllElements(Folder, Deserialize);

    internal static VaultFolder Deserialize(XElement element)
        => new(element.ParseAttributeValue("Id", VaultFolderId.Parse),
            element.GetAttributeValue("Name"),
            element.GetAttributeValue("FullName"),
            element.ParseAttributeValue("ParId", VaultFolderId.Parse),
            element.ParseAttributeValue("CreateDate", DateTime.Parse),
            element.GetAttributeValue("CreateUserName"),
            element.ParseAttributeValue("CreateUserId", VaultUserId.Parse),
            element.ParseAttributeValue("NumClds", int.Parse),
            element.ParseAttributeValue("IsLib", bool.Parse),
            element.ParseAttributeValue("Cloaked", bool.Parse),
            element.ParseAttributeValue("Locked", bool.Parse),
            element.ParseElement("Cat", VaultEntityCategory.Deserialize));

    internal static XElement Serialize(VaultFolder folder, XNamespace @namespace)
        => new XElement(@namespace + "Folder")
            .AddAttribute("Id", folder.Id)
            .AddAttribute("Name", folder.Name)
            .AddAttribute("FullName", folder.Path)
            .AddAttribute("ParId", folder.ParentFolderId)
            .AddAttribute("CreateDate", folder.CreateDate)
            .AddAttribute("CreateUserName", folder.CreateUserName)
            .AddAttribute("CreateUserId", folder.CreateUserId)
            .AddAttribute("NumClds", folder.ChildFolderCount)
            .AddAttribute("IsLib", folder.IsLibraryFolder)
            .AddAttribute("Cloaked", folder.IsCloaked)
            .AddAttribute("Locked", folder.IsLocked)
            .AddElement(VaultEntityCategory.Serialize(folder.Category, @namespace));
}

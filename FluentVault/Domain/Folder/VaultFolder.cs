﻿using System.Xml.Linq;

using FluentVault.Common;
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
    VaultEntityCategory Category);

internal class VaultFolderSerializer : XDocumentSerializer<VaultFolder>
{
    private readonly VaultEntityCategorySerializer _categorySerializer;

    internal VaultFolderSerializer(XNamespace @namespace) : base("Folder", @namespace) 
    {
        _categorySerializer = new(Namespace);
    }

    internal override VaultFolder Deserialize(XElement element)
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
            element.ParseElement("Cat", _categorySerializer.Deserialize));

    internal override XElement Serialize(VaultFolder folder)
        => new XElement(Namespace + "Folder")
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
            .AddElement(_categorySerializer.Serialize(folder.Category));
}
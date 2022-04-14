using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultFolderSerializer : XElementSerializer<VaultFolder>
{
    private const string Folder = nameof(Folder);
    private const string FullName = nameof(FullName);
    private const string ParId = nameof(ParId);
    private const string NumClds = nameof(NumClds);
    private const string IsLib = nameof(IsLib);
    private const string Cloaked = nameof(Cloaked);
    private const string Locked = nameof(Locked);

    private readonly VaultEntityCategorySerializer _categorySerializer;

    public VaultFolderSerializer() : base(Folder, string.Empty)
    {
        _categorySerializer = new(Namespace);
    }

    internal VaultFolderSerializer(XNamespace @namespace) : base(Folder, @namespace) 
    {
        _categorySerializer = new(Namespace);
    }

    internal override VaultFolder Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultFolder.Id), VaultFolderId.Parse),
            element.GetAttributeValue(nameof(VaultFolder.Name)),
            element.GetAttributeValue(FullName),
            element.ParseAttributeValue(ParId, VaultFolderId.Parse),
            element.ParseAttributeValue(nameof(VaultFolder.CreateDate), DateTime.Parse),
            element.GetAttributeValue(nameof(VaultFolder.CreateUserName)),
            element.ParseAttributeValue(nameof(VaultFolder.CreateUserId), VaultUserId.Parse),
            element.ParseAttributeValue(NumClds, int.Parse),
            element.ParseAttributeValue(IsLib, bool.Parse),
            element.ParseAttributeValue(Cloaked, bool.Parse),
            element.ParseAttributeValue(Locked, bool.Parse),
            _categorySerializer.Deserialize(element));

    internal override XElement Serialize(VaultFolder folder)
        => BaseElement
            .AddAttribute(nameof(VaultFolder.Id), folder.Id)
            .AddAttribute(nameof(VaultFolder.Name), folder.Name)
            .AddAttribute(FullName, folder.Path)
            .AddAttribute(ParId, folder.ParentFolderId)
            .AddAttribute(nameof(VaultFolder.CreateDate), folder.CreateDate)
            .AddAttribute(nameof(VaultFolder.CreateUserName), folder.CreateUserName)
            .AddAttribute(nameof(VaultFolder.CreateUserId), folder.CreateUserId)
            .AddAttribute(NumClds, folder.ChildFolderCount)
            .AddAttribute(IsLib, folder.IsLibraryFolder)
            .AddAttribute(Cloaked, folder.IsCloaked)
            .AddAttribute(Locked, folder.IsLocked)
            .AddElement(_categorySerializer.Serialize(folder.Category));
}

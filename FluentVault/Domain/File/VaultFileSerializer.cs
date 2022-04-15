using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;

namespace FluentVault;

internal class VaultFileSerializer : XElementSerializer<VaultFile>
{
    private const string File = nameof(File);
    private const string Name = nameof(Name);
    private const string VerName = nameof(VerName);
    private const string VerNum = nameof(VerNum);
    private const string MaxCkInVerNum = nameof(MaxCkInVerNum);
    private const string Comm = nameof(Comm);
    private const string CkInDate = nameof(CkInDate);
    private const string ModDate = nameof(ModDate);
    private const string Cksum = nameof(Cksum);
    private const string CheckedOut = nameof(CheckedOut);
    private const string CkOutSpec = nameof(CkOutSpec);
    private const string CkOutMach = nameof(CkOutMach);
    private const string CkOutUserId = nameof(CkOutUserId);
    private const string Locked = nameof(Locked);
    private const string Hidden = nameof(Hidden);
    private const string Cloaked = nameof(Cloaked);
    private const string ControlledByChangeOrder = nameof(ControlledByChangeOrder);
    private const string DesignVisAttmtStatus = nameof(DesignVisAttmtStatus);

    private readonly VaultFileRevisionSerializer _revisionSerializer;
    private readonly VaultEntityLifeCycleSerializer _lifeCycleSerializer;
    private readonly VaultEntityCategorySerializer _categorySerializer;

    public VaultFileSerializer(XNamespace @namespace) : base(File, @namespace)
    {
        _revisionSerializer = new(Namespace);
        _lifeCycleSerializer = new(Namespace);
        _categorySerializer = new(Namespace);
    }

    internal override XElement Serialize(VaultFile file)
        => BaseElement
            .AddAttribute(nameof(VaultFile.Id), file.Id)
            .AddAttribute(Name, file.Filename)
            .AddAttribute(nameof(VaultFile.MasterId), file.MasterId)
            .AddAttribute(VerName, file.VersionName)
            .AddAttribute(VerNum, file.VersionNumber)
            .AddAttribute(MaxCkInVerNum, file.MaximumCheckInVersionNumber)
            .AddAttribute(Comm, file.Comment)
            .AddAttribute(CkInDate, file.CheckInDate)
            .AddAttribute(nameof(VaultFile.CreateDate), file.CreateDate)
            .AddAttribute(ModDate, file.ModifiedDate)
            .AddAttribute(nameof(VaultFile.CreateUserId), file.CreateUserId)
            .AddAttribute(nameof(VaultFile.CreateUserName), file.CreateUserName)
            .AddAttribute(Cksum, file.CheckSum)
            .AddAttribute(nameof(VaultFile.FileSize), file.FileSize)
            .AddAttribute(CheckedOut, file.IsCheckedOut)
            .AddAttribute(nameof(VaultFile.FolderId), file.FolderId)
            .AddAttribute(CkOutSpec, file.CheckedOutPath)
            .AddAttribute(CkOutMach, file.CheckedOutMachine)
            .AddAttribute(CkOutUserId, file.CheckedOutUserId)
            .AddAttribute(nameof(VaultFile.FileClass), file.FileClass)
            .AddAttribute(nameof(VaultFile.FileStatus), file.FileStatus)
            .AddAttribute(Locked, file.IsLocked)
            .AddAttribute(Hidden, file.IsHidden)
            .AddAttribute(Cloaked, file.IsCloaked)
            .AddAttribute(nameof(VaultFile.IsOnSite), file.IsOnSite)
            .AddAttribute(DesignVisAttmtStatus, file.DesignVisualAttachmentStatus)
            .AddAttribute(ControlledByChangeOrder, file.IsControlledByChangeOrder)
            .AddElement(_revisionSerializer.Serialize(file.Revision))
            .AddElement(_lifeCycleSerializer.Serialize(file.LifeCycle))
            .AddElement(_categorySerializer.Serialize(file.Category));

    internal override VaultFile Deserialize(XElement element)
        => new(element.ParseAttributeValue(nameof(VaultFile.Id), VaultFileId.Parse),
            element.GetAttributeValue(Name),
            element.ParseAttributeValue(nameof(VaultFile.MasterId), VaultMasterId.Parse),
            element.GetAttributeValue(VerName),
            element.ParseAttributeValue(VerNum, long.Parse),
            element.ParseAttributeValue(MaxCkInVerNum, long.Parse),
            element.GetAttributeValue(Comm),
            element.ParseAttributeValue(CkInDate, DateTime.Parse),
            element.ParseAttributeValue(nameof(VaultFile.CreateDate), DateTime.Parse),
            element.ParseAttributeValue(ModDate, DateTime.Parse),
            element.ParseAttributeValue(nameof(VaultFile.CreateUserId), VaultUserId.Parse),
            element.GetAttributeValue(nameof(VaultFile.CreateUserName)),
            element.ParseAttributeValue(Cksum, long.Parse),
            element.ParseAttributeValue(nameof(VaultFile.FileSize), long.Parse),
            element.ParseAttributeValue(CheckedOut, bool.Parse),
            element.ParseAttributeValue(nameof(VaultFile.FolderId), VaultFolderId.Parse),
            element.GetAttributeValue(CkOutSpec),
            element.GetAttributeValue(CkOutMach),
            element.ParseAttributeValue(CkOutUserId, VaultUserId.Parse),
            element.ParseAttributeValue(nameof(VaultFile.FileClass), x => VaultFileClass.FromName(x)),
            element.ParseAttributeValue(nameof(VaultFile.FileStatus), x => VaultFileStatus.FromName(x)),
            element.ParseAttributeValue(Locked, bool.Parse),
            element.ParseAttributeValue(Hidden, bool.Parse),
            element.ParseAttributeValue(Cloaked, bool.Parse),
            element.ParseAttributeValue(nameof(VaultFile.IsOnSite), bool.Parse),
            element.ParseAttributeValue(ControlledByChangeOrder, bool.Parse),
            element.GetAttributeValue(DesignVisAttmtStatus),
            _revisionSerializer.Deserialize(element),
            _lifeCycleSerializer.Deserialize(element),
            _categorySerializer.Deserialize(element));
}

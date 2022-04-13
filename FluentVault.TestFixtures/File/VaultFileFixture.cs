
using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.TestFixtures.File;
public class VaultFileFixture : VaultEntityRequestFixture<VaultFile>
{
    public VaultFileFixture() : base("FindFilesBySearchConditions", "http://AutodeskDM/Services/Document/1/7/2020/")
    {
        Fixture = new SmartEnumFixture();
    }

    public override XElement ParseXElement(VaultFile file)
    {
        XElement element = new(Namespace + "File");
        element.AddAttribute("Id", file.Id);
        element.AddAttribute("Name", file.Filename);
        element.AddAttribute("VerName", file.VersionName);
        element.AddAttribute("MasterId", file.MasterId);
        element.AddAttribute("VerNum", file.VersionNumber);
        element.AddAttribute("MaxCkInVerNum", file.MaximumCheckInVersionNumber);
        element.AddAttribute("CkInDate", file.CheckedInDate);
        element.AddAttribute("Comm", file.Comment);
        element.AddAttribute("CreateDate", file.CreatedDate);
        element.AddAttribute("CreateUserId", file.CreateUserId);
        element.AddAttribute("Cksum", file.CheckSum);
        element.AddAttribute("FileSize", file.FileSize);
        element.AddAttribute("ModDate", file.ModifiedDate);
        element.AddAttribute("CreateUserName", file.CreateUserName);
        element.AddAttribute("CheckedOut", file.IsCheckedOut);
        element.AddAttribute("FolderId", file.FolderId);
        element.AddAttribute("CkOutSpec", file.CheckedOutPath);
        element.AddAttribute("CkOutMach", file.CheckedOutMachine);
        element.AddAttribute("CkOutUserId", file.CheckedOutUserId);
        element.AddAttribute("FileClass", file.FileClass);
        element.AddAttribute("Locked", file.IsLocked);
        element.AddAttribute("Hidden", file.IsHidden);
        element.AddAttribute("Cloaked", file.IsCloaked);
        element.AddAttribute("FileStatus", file.FileStatus);
        element.AddAttribute("IsOnSite", file.IsOnSite);
        element.AddAttribute("DesignVisAttmtStatus", file.DesignVisualAttachmentStatus);
        element.AddAttribute("ControlledByChangeOrder", file.IsControlledByChangeOrder);
        element.Add(new VaultFileRevisionFixture(Namespace).ParseXElement(file.Revision));
        element.Add(new VaultFileLifeCycleFixture(Namespace).ParseXElement(file.LifeCycle));
        element.Add(new VaultEntityCategorySerializer(Namespace).Serialize(file.Category));

        return element;
    }
}

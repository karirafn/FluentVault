
using System.Xml.Linq;

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
        element.Add(new XAttribute("Id", file.Id));
        element.Add(new XAttribute("Name", file.Filename));
        element.Add(new XAttribute("VerName", file.VersionName));
        element.Add(new XAttribute("MasterId", file.MasterId));
        element.Add(new XAttribute("VerNum", file.VersionNumber));
        element.Add(new XAttribute("MaxCkInVerNum", file.MaximumCheckInVersionNumber));
        element.Add(new XAttribute("CkInDate", file.CheckedInDate));
        element.Add(new XAttribute("Comm", file.Comment));
        element.Add(new XAttribute("CreateDate", file.CreatedDate));
        element.Add(new XAttribute("CreateUserId", file.CreateUserId));
        element.Add(new XAttribute("Cksum", file.CheckSum));
        element.Add(new XAttribute("FileSize", file.FileSize));
        element.Add(new XAttribute("ModDate", file.ModifiedDate));
        element.Add(new XAttribute("CreateUserName", file.CreateUserName));
        element.Add(new XAttribute("CheckedOut", file.IsCheckedOut));
        element.Add(new XAttribute("FolderId", file.FolderId));
        element.Add(new XAttribute("CkOutSpec", file.CheckedOutPath));
        element.Add(new XAttribute("CkOutMach", file.CheckedOutMachine));
        element.Add(new XAttribute("CkOutUserId", file.CheckedOutUserId));
        element.Add(new XAttribute("FileClass", file.FileClass));
        element.Add(new XAttribute("Locked", file.IsLocked));
        element.Add(new XAttribute("Hidden", file.IsHidden));
        element.Add(new XAttribute("Cloaked", file.IsCloaked));
        element.Add(new XAttribute("FileStatus", file.FileStatus));
        element.Add(new XAttribute("IsOnSite", file.IsOnSite));
        element.Add(new XAttribute("DesignVisAttmtStatus", file.DesignVisualAttachmentStatus));
        element.Add(new XAttribute("ControlledByChangeOrder", file.IsControlledByChangeOrder));
        element.Add(new VaultFileRevisionFixture(Namespace).ParseXElement(file.Revision));
        element.Add(new VaultFileLifeCycleFixture(Namespace).ParseXElement(file.LifeCycle));
        element.Add(new VaultFileCategoryFixture(Namespace).ParseXElement(file.Category));

        return element;
    }
}

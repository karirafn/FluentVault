
using System.Xml.Linq;

using AutoFixture;

namespace FluentVault.TestFixtures;
public static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultFile> Files) GetVaultFileFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultFileClass.None);
        fixture.Register(() => VaultFileStatus.UpToDate);

        return CreateBody<VaultFile>(fixture, count, "FindFilesBySearchConditions", "http://AutodeskDM/Services/Document/1/7/2020/", CreateFileBody);
    }

    public static XDocument CreateFileXDocument(VaultFile file)
    {
        XDocument document = new("File");
        document.Add(new XAttribute("Id", file.Id));
        document.Add(new XAttribute("Name", file.Filename));
        document.Add(new XAttribute("VerName", file.VersionName));
        document.Add(new XAttribute("MasterId", file.MasterId));
        document.Add(new XAttribute("MaxCkInVerNum", file.MaximumCheckInVersionNumber));
        document.Add(new XAttribute("CkInDate", file.CheckedInDate));
        document.Add(new XAttribute("Comm", file.Comment));
        document.Add(new XAttribute("CreateDate", file.CreatedDate));
        document.Add(new XAttribute("CreateUserId", file.CreateUserId));
        document.Add(new XAttribute("Cksum", file.CheckSum));
        document.Add(new XAttribute("FileSize", file.FileSize));
        document.Add(new XAttribute("ModDate", file.ModifiedDate));
        document.Add(new XAttribute("CreateUserName", file.CreateUserName));
        document.Add(new XAttribute("CheckedOut", file.CheckedOutUserId));
        document.Add(new XAttribute("FolderId", file.FolderId));
        document.Add(new XAttribute("CkOutSpec", file.CheckedOutPath));
        document.Add(new XAttribute("CkOutMach", file.CheckedOutMachine));
        document.Add(new XAttribute("CkOutUserId", file.CheckedOutUserId));
        document.Add(new XAttribute("FileClass", file.FileClass));
        document.Add(new XAttribute("Locked", file.IsLocked));
        document.Add(new XAttribute("Hidden", file.IsHidden));
        document.Add(new XAttribute("Cloaked", file.IsCloaked));
        document.Add(new XAttribute("FileStatus", file.FileStatus));
        document.Add(new XAttribute("IsOnSite", file.IsOnSite));
        document.Add(new XAttribute("DesignVisAttmtStatus", file.DesignVisualAttachmentStatus));
        document.Add(new XAttribute("ControlledByChangeOrder", file.IsControlledByChangeOrder));
        document.Add(CreateFileRevisionXElement(file.Revision));
        document.Add(CreateFileLifeCycleXElement(file.LifeCycle));
        document.Add(CreateFileCategoryXElement(file.Category));

        return document;
    }

    public static string CreateFileBody(VaultFile file) => $@"<File
                    Id=""{file.Id}""
                    Name=""{file.Filename}""
                    VerName=""{file.VersionName}""
                    MasterId=""{file.MasterId}""
                    VerNum=""{file.VersionNumber}""
                    MaxCkInVerNum=""{file.MaximumCheckInVersionNumber}""
                    CkInDate=""{file.CheckedInDate}""
                    Comm=""{file.Comment}""
                    CreateDate=""{file.CreatedDate}""
                    CreateUserId=""{file.CreateUserId}""
                    Cksum=""{file.CheckSum}""
                    FileSize=""{file.FileSize}""
                    ModDate=""{file.ModifiedDate}""
                    CreateUserName=""{file.CreateUserName}""
                    CheckedOut=""{file.IsCheckedOut}""
                    FolderId=""{file.FolderId}""
                    CkOutSpec=""{file.CheckedOutPath}""
                    CkOutMach=""{file.CheckedOutMachine}""
                    CkOutUserId=""{file.CheckedOutUserId}""
                    FileClass=""{file.FileClass}""
                    Locked=""{file.IsLocked}""
                    Hidden=""{file.IsHidden}""
                    Cloaked=""{file.IsCloaked}""
                    FileStatus=""{file.FileStatus}""
                    IsOnSite=""{file.IsOnSite}""
                    DesignVisAttmtStatus=""{file.DesignVisualAttachmentStatus}""
                    ControlledByChangeOrder=""{file.IsControlledByChangeOrder}"">                                                          
                    {CreateFileRevisionBody(file.Revision)}
                    {CreateFileLifeCycleBody(file.LifeCycle)}
                    {CreateFileCategoryBody(file.Category)}
                </File>";
}

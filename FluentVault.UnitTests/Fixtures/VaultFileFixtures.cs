using System.Collections.Generic;

using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal static partial class VaultResponseFixtures
{
    public static (string Body, IEnumerable<VaultFile> Files) GetVaultFileFixtures(int count)
    {
        Fixture fixture = new();
        fixture.Register(() => VaultFileClass.None);
        fixture.Register(() => VaultFileStatus.UpToDate);

        return CreateBody<VaultFile>(fixture, count, "FindFilesBySearchConditions", "http://AutodeskDM/Services/Document/1/7/2020/", CreateFileBody);
    }

    private static string CreateFileBody(VaultFile file) => $@"<File
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
                    <FileRev
                        RevId=""{file.Revision?.Id}""
                        Label=""{file.Revision?.Label}""
                        MaxConsumeFileId=""{file.Revision?.MaximumConsumeFileId}""
                        MaxFileId=""{file.Revision?.MaximumFileId}""
                        RevDefId=""{file.Revision?.DefinitionId}""
                        MaxRevId=""{file.Revision?.MaximumRevisionId}""
                        Order=""{file.Revision?.Order}""/>
                    <FileLfCyc
                        LfCycStateId=""{file.Lifecycle?.StateId}""
                        LfCycDefId=""{file.Lifecycle?.DefinitionId}""
                        LfCycStateName=""{file.Lifecycle?.StateName}""
                        Consume=""{file.Lifecycle?.IsReleased}""
                        Obsolete=""{file.Lifecycle?.IsObsolete}""/>
                    <Cat
                        CatId=""{file.Category?.Id}""
                        CatName=""{file.Category?.Name}""/>
                </File>";
}

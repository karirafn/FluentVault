using System.Collections.Generic;
using System.Text;

using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal static class BodyFixtures
{
    public static (string Body, IEnumerable<VaultFile> Files) GetVaultFileFixtures(int count)
    {
        Fixture fixture = new();
        List<VaultFile> files = new();
        StringBuilder bodybuilder = new();

        bodybuilder.Append($@"
<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
    <s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
        <FindFilesBySearchConditionsResponse xmlns=""http://AutodeskDM/Services/Document/1/7/2020/"">
            <FindFilesBySearchConditionsResult>");

        for (int i = 0; i < count; i++)
        {
            VaultFile file = fixture.Create<VaultFile>();
            files.Add(file);
            bodybuilder.Append($@"<File
                    Id=""{file.Id}""
                    Name=""{file.Filename}""
                    VerName=""{file.VersionName}""
                    MasterId=""{file.MasterId}""
                    VerNum=""{file.VersionNumber}""
                    MaxCkInVerNum=""{file.MaximumCheckInVersionNumber}""
                    CkInDate=""{file.ChecedkInDate}""
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
                        Consume=""{file.Lifecycle?.IsConsume}""
                        Obsolete=""{file.Lifecycle?.IsObsolete}""/>
                    <Cat
                        CatId=""{file.Category?.Id}""
                        CatName=""{file.Category?.Name}""/>
                </File>");
        }

        bodybuilder.Append(@"</FindFilesBySearchConditionsResult>
            <bookmark/>
            <searchstatus TotalHits=""1"" IndxStatus=""IndexingComplete""/>
        </FindFilesBySearchConditionsResponse>
    </s:Body>
</s:Envelope>");

        return (bodybuilder.ToString(), files);
    }
}

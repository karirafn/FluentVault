using System;
using System.Collections.Generic;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;
using FluentAssertions.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class ExtensionTests
{
    [Fact]
    public void GetElementValue_ShouldReturnValue_WhenDocumentContainsElement()
    {
        // Arrange
        var expected = "someValue";
        var elementName = "SomeElement";
        var document = XDocument.Parse($"<{elementName}>{expected}</{elementName}>");

        // Act
        var result = document.GetElementValue(elementName);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GetElementValue_ShouldThrowException_WhenDocumentDoesNotContainElement()
    {
        // Arrange
        var elementToGet = "Something";
        var document = XDocument.Parse("<Root><Element>Value</Element></Root>");

        // Act

        // Assert
        Assert.Throws<KeyNotFoundException>(() => document.GetElementValue(elementToGet));
    }

    [Fact]
    public void GetElementValues_ShouldReturnValues_WhenDocumentContainsElements()
    {
        // Arrange
        var expected = ("someValue", "someOtherValue");
        var firstElement = "FirstElement";
        var secondElement = "SecondElement";
        var document = XDocument.Parse($"<Root><{firstElement}>{expected.Item1}</{firstElement}><{secondElement}>{expected.Item2}</{secondElement}></Root>");

        // Act
        var result = document.GetElementValues(firstElement, secondElement);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void ParseVaultFile_ShouldReturnFile_WhenParsingValidString()
    {
        // Arrange
        Fixture fixture = new();
        VaultFile expectedFile = fixture.Create<VaultFile>();

        var text = $@"
<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
    <s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
        <FindFilesBySearchConditionsResponse xmlns=""http://AutodeskDM/Services/Document/1/7/2020/"">
            <FindFilesBySearchConditionsResult>     
                <File
                    Id=""{expectedFile.Id}""
                    Name=""{expectedFile.Filename}""
                    VerName=""{expectedFile.VersionName}""
                    MasterId=""{expectedFile.MasterId}""
                    VerNum=""{expectedFile.VersionNumber}""
                    MaxCkInVerNum=""{expectedFile.MaximumCheckInVersionNumber}""
                    CkInDate=""{expectedFile.ChecedkInDate}""
                    Comm=""{expectedFile.Comment}""
                    CreateDate=""{expectedFile.CreatedDate}""
                    CreateUserId=""{expectedFile.CreateUserId}""
                    Cksum=""{expectedFile.CheckSum}""
                    FileSize=""{expectedFile.FileSize}""
                    ModDate=""{expectedFile.ModifiedDate}""
                    CreateUserName=""{expectedFile.CreateUserName}""
                    CheckedOut=""{expectedFile.IsCheckedOut}""
                    FolderId=""{expectedFile.FolderId}""
                    CkOutSpec=""{expectedFile.CheckedOutPath}""
                    CkOutMach=""{expectedFile.CheckedOutMachine}""
                    CkOutUserId=""{expectedFile.CheckedOutUserId}""
                    FileClass=""{expectedFile.FileClass}""
                    Locked=""{expectedFile.IsLocked}""
                    Hidden=""{expectedFile.IsHidden}""
                    Cloaked=""{expectedFile.IsCloaked}""
                    FileStatus=""{expectedFile.FileStatus}""
                    IsOnSite=""{expectedFile.IsOnSite}""
                    DesignVisAttmtStatus=""{expectedFile.DesignVisualAttachmentStatus}""
                    ControlledByChangeOrder=""{expectedFile.IsControlledByChangeOrder}"">                                                          
                    <FileRev
                        RevId=""{expectedFile.Revision?.Id}""
                        Label=""{expectedFile.Revision?.Label}""
                        MaxConsumeFileId=""{expectedFile.Revision?.MaximumConsumeFileId}""
                        MaxFileId=""{expectedFile.Revision?.MaximumFileId}""
                        RevDefId=""{expectedFile.Revision?.DefinitionId}""
                        MaxRevId=""{expectedFile.Revision?.MaximumRevisionId}""
                        Order=""{expectedFile.Revision?.Order}""/>
                    <FileLfCyc
                        LfCycStateId=""{expectedFile.Lifecycle?.StateId}""
                        LfCycDefId=""{expectedFile.Lifecycle?.DefinitionId}""
                        LfCycStateName=""{expectedFile.Lifecycle?.StateName}""
                        Consume=""{expectedFile.Lifecycle?.IsConsume}""
                        Obsolete=""{expectedFile.Lifecycle?.IsObsolete}""/>
                    <Cat
                        CatId=""{expectedFile.Category?.Id}""
                        CatName=""{expectedFile.Category?.Name}""/>
                </File>
            </FindFilesBySearchConditionsResult>
            <bookmark/>
            <searchstatus TotalHits=""1"" IndxStatus=""IndexingComplete""/>
        </FindFilesBySearchConditionsResponse>
    </s:Body>
</s:Envelope>";
        var document = XDocument.Parse(text);

        // Act
        var file = document.ParseVaultFile();

        // Assert
        file.Should().BeEquivalentTo(expectedFile, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FluentAssertions;
using FluentAssertions.Extensions;

using FluentVault.TestFixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.File;
public class VaultFileShould
{
    [Fact]
    public void ParseSingleFileFromXDocument()
    {
        // Arrange
        (string body, IEnumerable<VaultFile> expectation) = VaultResponseFixtures.GetVaultFileFixtures(1);
        XDocument document = XDocument.Parse(body);

        // Act
        VaultFile result = VaultFile.ParseSingle(document);

        // Assert
        result.Should().BeEquivalentTo(expectation.Single(), options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }

    [Fact]
    public void ParseAllFilesFromXDocument()
    {
        // Arrange
        int fileCount = 3;
        (string body, IEnumerable<VaultFile> expectation) = VaultResponseFixtures.GetVaultFileFixtures(fileCount);
        XDocument document = XDocument.Parse(body);

        // Act
        IEnumerable<VaultFile> result = VaultFile.ParseAll(document);

        // Assert
        result.Should().HaveCount(fileCount);
        result.Should().BeEquivalentTo(expectation, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }
}

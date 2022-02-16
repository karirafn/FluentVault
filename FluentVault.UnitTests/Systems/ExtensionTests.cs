using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;
using FluentAssertions.Extensions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems;

public class ExtensionTests
{
    [Fact]
    public void GetElementValue_ShouldReturnValue_WhenDocumentContainsElement()
    {
        // Arrange
        var expectation = "someValue";
        var elementName = "SomeElement";
        var document = XDocument.Parse($"<{elementName}>{expectation}</{elementName}>");

        // Act
        var result = document.GetElementValue(elementName);

        // Assert
        result.Should().Be(expectation);
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
        var expectation = ("someValue", "someOtherValue");
        var firstElement = "FirstElement";
        var secondElement = "SecondElement";
        var document = XDocument.Parse($"<Root><{firstElement}>{expectation.Item1}</{firstElement}><{secondElement}>{expectation.Item2}</{secondElement}></Root>");

        // Act
        var result = document.GetElementValues(firstElement, secondElement);

        // Assert
        result.Should().Be(expectation);
    }

    [Fact]
    public void ParseVaultFile_ShouldReturnFile_WhenParsingValidString()
    {
        // Arrange
        var (body, expectation) = BodyFixtures.GetVaultFileFixtures(1);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseVaultFile();

        // Assert
        result.Should().BeEquivalentTo(expectation.Single(), options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }

    [Fact]
    public void ParseAllVaultFiles_ShouldReturnAllFiles_WhenParsingValidString()
    {
        // Arrange
        Fixture fixture = new();
        int fileCount = 3;
        var (body, expectation) = BodyFixtures.GetVaultFileFixtures(fileCount);
        var document = XDocument.Parse(body);

        // Act
        var result = document.ParseAllVaultFiles();

        // Assert
        result.Should().HaveCount(fileCount);
        result.Should().BeEquivalentTo(expectation, options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
    }
}

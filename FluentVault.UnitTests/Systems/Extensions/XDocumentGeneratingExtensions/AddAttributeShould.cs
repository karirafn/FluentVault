using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddAttributeShould
{
    [Fact]
    public void AddAttributeToElement()
    {
        // Arrange
        string elementName = "Element";
        string attributeName = "Attribute";
        string attributeValue = "Value";
        XElement element = new(elementName);
        string expectation = @"<Element Attribute=""Value"" />";

        // Act
        element.AddAttribute(attributeName, attributeValue);

        // Assert
        element.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

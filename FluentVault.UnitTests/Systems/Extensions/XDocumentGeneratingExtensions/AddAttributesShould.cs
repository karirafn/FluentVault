using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddAttributesShould
{
    [Fact]
    public void AddAttributesToElement()
    {
        // Arrange
        string elementName = "Element";
        XElement element = new(elementName);
        Dictionary<string, object> attributes = new() { ["A"] = "1", ["B"] = "2", };
        string expectation = @"<Element A=""1"" B=""2"" />";

        // Act
        element.AddAttributes(attributes);

        // Assert
        element.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

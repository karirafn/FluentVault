using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddElementWithAttributesShould
{
    [Fact]
    public void AddElementWithAttributesToParent()
    {
        // Arrange
        string parentName = "Parent";
        string childName = "Child";
        XNamespace ns = "Namespace";
        Dictionary<string, string> attributes = new()
        {
            ["A"] = "1",
            ["B"] = "2",
        };
        XElement parent = new(ns + parentName);
        string expectation = @"<Parent xmlns=""Namespace""><Child A=""1"" B=""2"" /></Parent>";

        // Act
        parent.AddElementWithAttributes(ns, childName, attributes);

        // Assert
        parent.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

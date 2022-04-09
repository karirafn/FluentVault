using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddElementsWithAttributesShould
{
    [Fact]
    public void AddElementWithAttributesToParent()
    {
        // Arrange
        string parentName = "Parent";
        string childName = "Child";
        XNamespace ns = "Namespace";
        List<Dictionary<string, object>> attributeSets = new()
        {
            new() { ["A"] = "1", ["B"] = "2", },
            new() { ["A"] = "3", ["B"] = "4", }
        };
        XElement parent = new(ns + parentName);
        string expectation = @"<Parent xmlns=""Namespace""><Child A=""1"" B=""2"" /><Child A=""3"" B=""4"" /></Parent>";

        // Act
        parent.AddElementsWithAttributes(ns, childName, attributeSets);

        // Assert
        parent.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

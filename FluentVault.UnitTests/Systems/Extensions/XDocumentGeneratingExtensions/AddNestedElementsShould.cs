using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddNestedElementsShould
{
    [Fact]
    public void AddNestedElementsToParent()
    {
        // Arrange
        string rootName = "Parent";
        string nestName = "Nested";
        string childName = "Child";
        List<string> childValues = new() { "A", "B" };
        XNamespace ns = "Namespace";
        XElement parent = new(ns + rootName);

        string expectation = @"<Parent xmlns=""Namespace""><Child><Nested>A</Nested><Nested>B</Nested></Child></Parent>";

        // Act
        parent.AddNestedElements(ns, childName, nestName, childValues);

        // Assert
        parent.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

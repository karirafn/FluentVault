using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddElementsShould
{
    [Fact]
    public void AddElementsToParent()
    {
        // Arrange
        string parentName = "Parent";
        string childName = "Child";
        List<string> childValues = new() { "A", "B" };
        XNamespace ns = "Namespace";
        XElement parent = new(ns + parentName);

        string expectation = @"<Parent xmlns=""Namespace""><Child>A</Child><Child>B</Child></Parent>";

        // Act
        parent.AddElements(ns, childName, childValues);

        // Assert
        parent.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

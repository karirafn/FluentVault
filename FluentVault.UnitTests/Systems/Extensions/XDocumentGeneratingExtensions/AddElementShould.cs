using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.XDocumentGeneratingExtensions;

public class AddElementShould
{
    [Fact]
    public void AddElementToParent()
    {
        // Arrange
        string parentName = "Parent";
        string childName = "Child";
        string childValue = "Value";
        XNamespace ns = "Namespace";
        XElement parent = new(ns + parentName);

        string expectation = @"<Parent xmlns=""Namespace""><Child>Value</Child></Parent>";

        // Act
        parent.AddElement(ns, childName, childValue);

        // Assert
        parent.ToString(SaveOptions.DisableFormatting).Should().Be(expectation);
    }
}

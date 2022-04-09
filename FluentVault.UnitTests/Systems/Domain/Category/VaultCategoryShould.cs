using System.Collections.Generic;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.TestFixtures.Category;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Category;

public class VaultCategoryShould
{
    private static readonly VaultCategoryFixture _fixture = new();

    [Fact]
    public void ParseCategoriesFromXDocument()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultCategory> expectation = _fixture.CreateMany(count);
        XDocument document = _fixture.ParseXDocument(expectation);

        // Act
        IEnumerable<VaultCategory> result = VaultCategory.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}

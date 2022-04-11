using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Domain.Search;
using FluentVault.TestFixtures.Search;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Search;
public class VaultFileSearchResultShould
{
    private static readonly VaultFileSearchResultFixture _fixture = new();

    [Fact]
    public void ParseFromXElement()
    {
        // Arrange
        VaultFileSearchResult expectation = _fixture.Create();
        XDocument element = _fixture.ParseXDocument(expectation);

        // Act
        VaultFileSearchResult result = VaultFileSearchResult.Parse(element);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}

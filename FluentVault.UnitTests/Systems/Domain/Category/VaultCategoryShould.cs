﻿using System.Xml.Linq;

using FluentAssertions;

using FluentVault.UnitTests.Fixtures;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.Category;

public class VaultCategoryShould
{
    [Fact]
    public void ParseCategoriesFromXDocument()
    {
        // Arrange
        var (body, expectation) = VaultResponseFixtures.GetVaultCategoryFixtures(5);
        var document = XDocument.Parse(body);

        // Act
        var result = VaultCategoryConfiguration.ParseAll(document);

        // Assert
        result.Should().BeEquivalentTo(expectation);
    }
}
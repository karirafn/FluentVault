
using System;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Domain.User;
public class VaultEntityCategorySerializerShould
{
    private static readonly Fixture _fixture = new();
    private static readonly VaultEntityCategorySerializer _serializer = new(string.Empty);

    [Fact]
    public void Serialize()
    {
        // Arrange
        VaultEntityCategory expectation = _fixture.Create<VaultEntityCategory>();

        // Act
        Func<XElement> func = () => _serializer.Serialize(expectation);

        // Assert
        func.Should().NotThrow();
    }

    [Fact]
    public void Deserialize()
    {
        // Arrange
        VaultEntityCategory expectation = _fixture.Create<VaultEntityCategory>();
        XElement element = _serializer.Serialize(expectation);

        // Act
        Func<VaultEntityCategory> func = () => _serializer.Deserialize(element);

        // Assert
        func.Should().NotThrow();
    }
}

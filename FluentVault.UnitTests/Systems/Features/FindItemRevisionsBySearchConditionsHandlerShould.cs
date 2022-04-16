﻿using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Domain.Search.Items;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class FindItemRevisionsBySearchConditionsHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly FindItemRevisionsBySearchConditionsSerializer _serializer = new();

    [Fact]
    public async Task CallVaultService()
    {
        // Arrange
        VaultSearchItemsResponse expectation = _fixture.Create<VaultSearchItemsResponse>();
        XDocument response = _serializer.Serialize(expectation);

        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        FindItemRevisionsBySearchConditionsQuery query = _fixture.Create<FindItemRevisionsBySearchConditionsQuery>();
        FindItemRevisionsBySearchConditionsHandler sut = new(vaultService.Object);

        // Act
        VaultSearchItemsResponse result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}
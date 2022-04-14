using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using AutoFixture;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetAllPropertyDefinitionInfosHandlerShould
{
    private static readonly SmartEnumFixture _fixture = new();
    private static readonly GetAllPropertyDefinitionInfosSerializer _serialiser = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultProperty> expectation = _fixture.CreateMany<VaultProperty>(count);
        XDocument response = _serialiser.Serialize(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetAllPropertyDefinitionInfosQuery query = new();
        GetAllPropertyDefinitionInfosHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultProperty> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}

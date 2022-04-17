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

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        IEnumerable<VaultProperty> expectation = _fixture.CreateMany<VaultProperty>();
        Mock<IVaultService> vaultService = new();

        GetAllPropertyDefinitionInfosQuery query = new();
        GetAllPropertyDefinitionInfosHandler sut = new(vaultService.Object);

        XDocument response = sut.Serializer.Serialize(expectation);
        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        // Act
        IEnumerable<VaultProperty> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}

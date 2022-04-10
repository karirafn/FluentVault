using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures.Property;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetAllPropertyDefinitionInfosHandlerShould
{
    private static readonly VaultPropertyFixture _fixture = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultProperty> expectation = _fixture.CreateMany(count);
        XDocument response = _fixture.ParseXDocument(expectation);
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

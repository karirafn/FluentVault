using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using FluentAssertions;

using FluentVault.Common;
using FluentVault.Features;
using FluentVault.TestFixtures.LifeCycle;
using FluentVault.UnitTests.Helpers;

using Moq;

using Xunit;

namespace FluentVault.UnitTests.Systems.Features;
public class GetAllLifeCycleDefinitionsHandlerShould
{
    private static readonly VaultLifeCycleDefinitionFixture _fixture = new();

    [Fact]
    public async Task ValVaultService()
    {
        // Arrange
        int count = 5;
        IEnumerable<VaultLifeCycleDefinition> expectation = _fixture.CreateMany(count);
        XDocument response = _fixture.ParseXDocument(expectation);
        Mock<IVaultService> vaultService = new();

        vaultService.Setup(VaultServiceExpressions.SendAsync)
            .ReturnsAsync(response);

        GetAllLifeCycleDefinitionsQuery query = new();
        GetAllLifeCycleDefinitionsHandler sut = new(vaultService.Object);

        // Act
        IEnumerable<VaultLifeCycleDefinition> result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectation);
        vaultService.Verify(VaultServiceExpressions.SendAsync, Times.Once());
    }
}

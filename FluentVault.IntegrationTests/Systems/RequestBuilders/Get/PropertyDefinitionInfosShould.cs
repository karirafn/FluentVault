using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.RequestBuilders;

using MediatR;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class PropertyDefinitionInfosShould
{
    [Fact]
    public async Task ReturnAllLifeCycleDefinitions()
    {
        // Arrange
        await using VaultServiceProvider provider = new();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        GetRequestBuilder sut = new(mediator);

        // Act
        IEnumerable<VaultProperty> result = await sut.PropertyDefinitionInfos(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.RequestBuilders;

using MediatR;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class CategoryConfigurationsShould
{
    [Fact]
    public async Task ReturnAllCategoryConfigurations()
    {
        // Arrange
        await using VaultServiceProvider provider = new();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        GetRequestBuilder sut = new(mediator);

        // Act
        IEnumerable<VaultCategory> result = await sut.CategoryConfigurations(CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
}

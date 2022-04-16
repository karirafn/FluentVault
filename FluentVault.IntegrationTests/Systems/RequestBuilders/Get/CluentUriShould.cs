﻿using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Fixtures;
using FluentVault.RequestBuilders;

using MediatR;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.RequestBuilders.Get;
public class CluentUriShould
{
    private static readonly VaultTestData _testData = new();

    [Fact]
    public async Task ReturnFile()
    {
        // Arrange
        await using VaultServiceProvider provider = new();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        GetRequestBuilder sut = new(mediator);

        // Act
        (Uri ThinClient, Uri ThickClient) result = await sut.ClientUris(new(_testData.TestPartMasterId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}
using System;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.VaultRequestTests;

public class UpdateFileLifecycleStateTests : BaseRequestTest
{
    [Fact]
    public async Task UpdateFileLifecycleStateBuilder_Should()
    {
        // Arrange
        string comment = new Guid().ToString();

        var oldFile = await _vault.Search.Files
            .ForValueContaining(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync();

        if (oldFile?.Lifecycle?.StateId.Equals(_v.DefaultLifecycleStateId) is false)
            oldFile = await _vault.Update.File.LifecycleState
            .WithMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.DefaultLifecycleStateId)
            .WithComment(comment);

        // Act
        var newFile = await _vault.Update.File.LifecycleState
            .WithMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.TestingLifecycleStateId)
            .WithComment(comment);

        // Assert
        oldFile?.Lifecycle?.StateId.Should().NotBe(newFile.Lifecycle?.StateId);
        newFile.Lifecycle?.StateId.Should().Be(_v.TestingLifecycleStateId);
        newFile.Comment.Should().Be(comment);
    }
}

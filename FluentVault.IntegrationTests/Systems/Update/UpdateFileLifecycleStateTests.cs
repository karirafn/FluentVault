using System;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Update;

public class UpdateFileLifecycleStateTests : BaseRequestTest
{
    [Fact]
    public async Task UpdateFileLifecycleStateByMasterId_ShouldReturnUpdatedFile_WhenInputIsValid()
    {
        // Arrange
        string comment = Guid.NewGuid().ToString();

        var oldFile = await _vault.Search.Files
            .ForValueContaining(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync();

        if (oldFile?.Lifecycle.StateId.Equals(_v.DefaultLifecycleStateId) is false)
            oldFile = await _vault.Update.File.LifecycleState
            .ByMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.DefaultLifecycleStateId)
            .WithComment(comment);

        // Act
        var newFile = await _vault.Update.File.LifecycleState
            .ByMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.TestingLifecycleStateId)
            .WithComment(comment);

        // Assert
        oldFile?.Lifecycle.StateId.Should().NotBe(newFile.Lifecycle?.StateId);
        newFile?.Lifecycle?.StateId.Should().Be(_v.TestingLifecycleStateId);
        newFile?.Comment.Should().Be(comment);

        _ = await _vault.Update.File.LifecycleState
            .ByMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.DefaultLifecycleStateId)
            .WithComment(comment);
    }

    [Fact]
    public async Task UpdateFileLifecycleStateByFilename_ShouldReturnUpdatedFile_WhenInputIsValid()
    {
        // Arrange
        var oldFile = await _vault.Search.Files
            .ForValueContaining(_v.TestPartFilename)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchSingleAsync();

        if (oldFile?.Lifecycle.StateId.Equals(_v.DefaultLifecycleStateId) is false)
            oldFile = await _vault.Update.File.LifecycleState
            .ByMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.DefaultLifecycleStateId)
            .WithoutComment();

        // Act
        var newFile = await _vault.Update.File.LifecycleState
            .ByFilename(_v.TestPartFilename)
            .ToStateWithId(_v.TestingLifecycleStateId)
            .WithoutComment();

        // Assert
        oldFile?.Lifecycle.StateId.Should().NotBe(newFile.Lifecycle?.StateId);
        newFile?.Lifecycle?.StateId.Should().Be(_v.TestingLifecycleStateId);
        newFile?.Comment.Should().Be(string.Empty);

        _ = await _vault.Update.File.LifecycleState
            .ByMasterId(_v.TestPartMasterId)
            .ToStateWithId(_v.DefaultLifecycleStateId)
            .WithoutComment();
    }
}

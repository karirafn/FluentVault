using System;
using System.Threading.Tasks;

using FluentAssertions;

using FluentVault.IntegrationTests.Helpers;

using Xunit;

namespace FluentVault.UnitTests.VaultRequestBuilderTests;

public class UpdateFileLifecycleStateTests
{
    [Fact]
    public async Task UpdateFileLifecycleStateBuilder_Should()
    {
        // Arrange
        var v = ConfigurationHelper.GetVaultOptions();
        string comment = new Guid().ToString();

        using var vault = await Vault.SignIn
            .ToVault(v.Server, v.Database)
            .WithCredentials(v.Username, v.Password);

        var oldFile = await vault.Search.Files
            .ForValueContaining(v.TestPartFilename)
            .InProperty(SearchStringProperty.Filename)
            .SearchAsync();

        if (oldFile.Lifecycle?.StateId.Equals(v.DefaultLifecycleStateId) is false)
            oldFile = await vault.Update.File.LifecycleState
            .WithMasterId(v.TestPartMasterId)
            .ToStateWithId(v.DefaultLifecycleStateId)
            .WithComment(comment);

        // Act
        var newFile = await vault.Update.File.LifecycleState
            .WithMasterId(v.TestPartMasterId)
            .ToStateWithId(v.TestingLifecycleStateId)
            .WithComment(comment);

        // Assert
        oldFile.Lifecycle?.StateId.Should().NotBe(newFile.Lifecycle?.StateId);
        newFile.Lifecycle?.StateId.Should().Be(v.TestingLifecycleStateId);
        newFile.Comment.Should().Be(comment);
    }
}

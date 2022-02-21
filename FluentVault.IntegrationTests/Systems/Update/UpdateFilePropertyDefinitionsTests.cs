using System.Threading.Tasks;

using Xunit;

namespace FluentVault.IntegrationTests.Systems.Update;

public class UpdateFilePropertyDefinitionsTests : BaseRequestTest
{
    [Fact]
    public async Task UpdatFilePropertyDefinitions_ShouldUpdate_WhenInputIsValid()
    {
        // Arrange

        // Act
        var file = await _vault.Update.File.PropertyDefinitions
            .ByFileMasterId(_v.TestPartMasterId)
            .AddPropertyByName("Color")
            .ExecuteAsync();

        // Assert

    }
}

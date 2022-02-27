using System.Text;

using FluentAssertions;

using FluentVault.Common.Extensions;

using Xunit;

namespace FluentVault.UnitTests.Systems.Extensions.StringBuilderExtensions;

public class AppendRequestCommandShould
{
    [Fact]
    public void AppeendNothing_WhenCommandIsEmpty()
    {
        // Arrange
        string name = "Name";
        string command = string.Empty;
        StringBuilder builder = new();

        // Act
        builder.AppendRequestCommand(name, command);

        // Assert
        builder.ToString().Should().BeEmpty();
    }

    [Fact]
    public void AppeendCommand_WhenCommandIsValid()
    {
        // Arrange
        string name = "Name";
        string command = "Command";
        StringBuilder builder = new();
        string expectation = "?op=Name&currentCommand=Command";

        // Act
        builder.AppendRequestCommand(name, command);

        // Assert
        builder.ToString().Should().Be(expectation);
    }
}

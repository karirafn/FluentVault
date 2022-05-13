using System.Text;

namespace FluentVault.Extensions;

internal static class StringBuilderExtensions
{
    internal static StringBuilder AppendRequestCommand(this StringBuilder builder, string command)
        => string.IsNullOrEmpty(command)
        ? builder
        : builder.Append("&currentCommand=")
            .Append(command);
}

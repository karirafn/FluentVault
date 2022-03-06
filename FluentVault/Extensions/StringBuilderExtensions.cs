using System.Text;

namespace FluentVault.Extensions;

internal static class StringBuilderExtensions
{
    internal static StringBuilder AppendRequestCommand(this StringBuilder builder, string name, string command)
        => string.IsNullOrEmpty(command)
        ? builder
        : builder.Append("?op=")
            .Append(name)
            .Append("&currentCommand=")
            .Append(command);
}

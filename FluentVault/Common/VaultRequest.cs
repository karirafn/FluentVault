using System.Text;
using System.Xml.Linq;

using FluentVault.Extensions;

namespace FluentVault.Common;

internal class VaultRequest
{
    public VaultRequest(string operation, string version, string service, string command, string @namespace)
    {
        Operation = operation;
        StringBuilder namespaceBuilder = new StringBuilder().Append("http://AutodeskDM/").Append(@namespace);
        Namespace = namespaceBuilder.ToString();
        XNamespace = $"{Namespace}/";

        SoapAction = new StringBuilder()
            .Append(namespaceBuilder)
            .Append('/')
            .Append(service)
            .Append('/')
            .Append(operation)
            .ToString();

        Uri = new StringBuilder()
            .Append("AutodeskDM/Services/")
            .Append(version)
            .Append('/')
            .Append(service)
            .Append(".svc?op=")
            .Append(operation)
            .AppendRequestCommand(command)
            .ToString();
    }

    public string Operation { get; init; }
    public string SoapAction { get; init; }
    public string Uri { get; init; }
    public string Namespace { get; init; }
    public XNamespace XNamespace { get; init; }
}

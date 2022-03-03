using System.Text;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.SOAP;

internal record SoapRequestData(string Name, string Version, string Service, string Command, string Namespace)
{
    internal string SoapAction
        => new StringBuilder()
            .Append("http://AutodeskDM/")
            .Append(NamespaceBuilder)
            .ToString();

    internal Uri Uri
        => new(new StringBuilder().Append("AutodeskDM/Services/")
            .Append(Version)
            .Append('/')
            .Append(Service)
            .Append(".svc")
            .AppendRequestCommand(Name, Command)
            .ToString());

    internal StringBuilder NamespaceBuilder
        => new StringBuilder().Append("http://AutodeskDM/").Append(Namespace);
}

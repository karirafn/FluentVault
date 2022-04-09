using System.Text;

using FluentValidation;

using FluentVault.Extensions;

namespace FluentVault.Common;

internal class VaultRequest
{
    private readonly string _version;
    private readonly string _service;
    private readonly string _command;
    private readonly string _namespace;

    internal VaultRequest(string operation, string version, string service, string command, string @namespace)
    {
        (Operation, _version, _service, _command, _namespace) = (operation, version, service, command, @namespace);
        new SoapRequestDataValidator().ValidateAndThrow(this);
    }

    internal string Operation { get; init; }

    internal string SoapAction
        => new StringBuilder()
            .Append(NamespaceBuilder)
            .Append('/')
            .Append(_service)
            .Append('/')
            .Append(Operation)
            .ToString();

    internal string Uri
        => new StringBuilder()
            .Append("AutodeskDM/Services/")
            .Append(_version)
            .Append('/')
            .Append(_service)
            .Append(".svc")
            .AppendRequestCommand(Operation, _command)
            .ToString();

    internal string Namespace
        => NamespaceBuilder.ToString();

    private StringBuilder NamespaceBuilder
        => new StringBuilder()
            .Append("http://AutodeskDM/")
            .Append(_namespace);

    private class SoapRequestDataValidator : AbstractValidator<VaultRequest>
    {
        public SoapRequestDataValidator()
        {
            RuleFor(x => x.Operation).Matches(@"^(Get|Find|Update|Sign)[A-Z]\w+$");
            RuleFor(x => x._version).Matches(@"^(Filestore\/)?v\d{2}(_\d)?$");
            RuleFor(x => x._service).Matches(@"^\w+Service(Extensions)?$");
            RuleFor(x => x._command).Matches(@"^$|^\w+\.\w+\.\w+\.\w+$");
            RuleFor(x => x._namespace).Matches(@"^(Services|Filestore)\/\w+\/\b([1-9]|12[1-9]|3[01])\b\/\b([0-9]|1[02])\b\/\d{4}$");
        }
    }
}

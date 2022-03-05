using System.Text;

using FluentValidation;

using FluentVault.Common.Extensions;

namespace FluentVault.Domain.SOAP;

internal class SoapRequestData
{
    private readonly string _version;
    private readonly string _service;
    private readonly string _command;
    private readonly string _namespace;

    internal SoapRequestData(string name, string version, string service, string command, string @namespace)
    {

        (Name, _version, _service, _command, _namespace) = (name, version, service, command, @namespace);
        new SoapRequestDataValidator().ValidateAndThrow(this);
    }

    internal string Name { get; init; }

    internal string SoapAction
        => new StringBuilder()
            .Append(NamespaceBuilder)
            .Append('/')
            .Append(_service)
            .Append('/')
            .Append(Name)
            .ToString();

    internal string Uri
        => new StringBuilder()
            .Append("AutodeskDM/Services/")
            .Append(_version)
            .Append('/')
            .Append(_service)
            .Append(".svc")
            .AppendRequestCommand(Name, _command)
            .ToString();

    internal string Namespace
        => NamespaceBuilder.ToString();

    private StringBuilder NamespaceBuilder
        => new StringBuilder()
            .Append("http://AutodeskDM/")
            .Append(_namespace);

    private class SoapRequestDataValidator : AbstractValidator<SoapRequestData>
    {
        public SoapRequestDataValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x._version).NotEmpty().Matches(@"^(Filestore\/)?v\d{2}(_\d)?$");
            RuleFor(x => x._service).NotEmpty().Matches(@"^[A-Z][a-z]+Service(Extensions)?$");
            RuleFor(x => x._command).NotNull().Matches(@"^$|^\w+\.\w+\.\w+\.\w+$");
            RuleFor(x => x._namespace).NotEmpty();
        }
    }
}

namespace FluentVault.Domain.SOAP;

internal record SoapRequestData(string Name, string Version, string Service, string Command, string Namespace);

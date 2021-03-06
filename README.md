# FluentVault
![Dotnet](https://github.com/karirafn/fluentvault/actions/workflows/dotnet.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/dt/fluentvault.svg)](https://www.nuget.org/packages/fluentvault)
[![NuGet](https://img.shields.io/nuget/vpre/fluentvault.svg)](https://www.nuget.org/packages/fluentvault)

A fluent syntax wrapper for SOAP requests to Autodesk Vault.

## Using FluentVault
Register FluentVault into the dependency injection container.

```c#
builder.Services.AddFluentVault(options =>
{
    options.Server = "serverName";
    options.Database = "databaseName";
    options.Username = "username";
    options.Password = "password";
});
```

Then inject it into your class.

```c#
public class MyClass
{
    private readonly IVaultClient _vaultClient;
    
    public MyClass(IVaultClient vaultClient)
    {
        _vaultClient = vaultClient;
    }
}
```

Example request

```c#
var response = _vaultClient.Search.Files
    .BySystemProperty(VaultSystemProperty.FileName)
    .Containing("part.ipt")
    .GetPagedResultAsync();
```

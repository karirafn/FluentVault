# FluentVault
![Dotnet](https://github.com/karirafn/FluentVault/workflows/dotnet/badge.svg)
A wrapper for Autodesk Vault's API using the fluent syntax.

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
    .ForValueContaining("part.ipt")
    .InSystemProperty(StringSearchProperty.FileName)
    .WithPaging();
```

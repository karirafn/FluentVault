# FluentVault
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

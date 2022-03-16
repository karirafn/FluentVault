
using Ardalis.SmartEnum;

namespace FluentVault;
public sealed class VaultAuthenticationType : SmartEnum<VaultAuthenticationType>
{
    public static readonly VaultAuthenticationType ActiveDirectory = new("ActiveDir", 1);
    public static readonly VaultAuthenticationType AutodeskAccount = new("Autodesk", 2);
    public static readonly VaultAuthenticationType VaultAccount = new("Vault", 2);

    public VaultAuthenticationType(string name, int value) : base(name, value) { }
}

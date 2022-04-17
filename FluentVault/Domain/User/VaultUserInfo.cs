namespace FluentVault;
public record VaultUserInfo(
    VaultUser User,
    IEnumerable<VaultRole> Roles,
    IEnumerable<VaultInstance> Vaults,
    IEnumerable<VaultGroup> Groups);

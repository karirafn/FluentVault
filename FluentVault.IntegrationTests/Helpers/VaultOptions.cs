namespace FluentVault.IntegrationTests.Helpers;

public class VaultOptions
{
    public string Server { get; init; } = string.Empty;
    public string Database { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string TestPartFilename { get; init; } = string.Empty;
    public long TestPartMasterId { get; init; }
    public string TestPartDescription { get; init; } = string.Empty;
    public long DefaultLifecycleStateId { get; init; }
    public long TestingLifecycleStateId { get; init; }
}

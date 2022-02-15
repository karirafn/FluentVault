namespace FluentVault.IntegrationTests.Helpers;

public class VaultOptions
{
    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TestPartFilename { get; set; } = string.Empty;
    public long TestPartMasterId { get; set; }
    public long DefaultLifecycleStateId { get; set; }
    public long TestingLifecycleStateId { get; set; }
}

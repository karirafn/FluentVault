namespace FluentVault;

public class VaultOptions
{
    public VaultOptions() { }

    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool AutoLogin { get; set; } = true;
}

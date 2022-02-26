namespace FluentVault;

public class VaultOptions
{
    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public VaultOptions(string server, string database, string username, string password)
    {
        Server = server;
        Database = database;
        Username = username;
        Password = password;
    }
}

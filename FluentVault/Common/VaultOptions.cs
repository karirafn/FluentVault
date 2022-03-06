using FluentValidation;

namespace FluentVault;

internal class VaultOptionsValidator : AbstractValidator<VaultOptions>
{
    public VaultOptionsValidator()
    {
        RuleFor(x => x.Server).NotEmpty();
        RuleFor(x => x.Database).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotNull();
    }
}

public class VaultOptions
{
    public VaultOptions() { }

    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool AutoLogin { get; set; } = true;
}

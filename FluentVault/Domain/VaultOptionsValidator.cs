
using FluentValidation;

namespace FluentVault.Domain;

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


using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

internal record SignInCommand(VaultOptions VaultOptions) : IRequest<VaultSessionCredentials>;

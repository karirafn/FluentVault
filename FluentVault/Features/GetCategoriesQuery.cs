using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

public record GetCategoriesQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultCategory>>;

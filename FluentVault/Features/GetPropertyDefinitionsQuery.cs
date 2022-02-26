
using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

internal record GetPropertyDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultPropertyDefinition>>;

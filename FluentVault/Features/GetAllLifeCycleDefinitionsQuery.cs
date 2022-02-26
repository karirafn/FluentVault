using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

internal record GetAllLifeCycleDefinitionsQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultLifeCycle>>;

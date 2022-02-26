
using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFileLifeCycleStateCommand(
    IEnumerable<string> FileNames,
    IEnumerable<long> MasterIds,
    IEnumerable<long> StateIds,
    string Comment,
    VaultSessionCredentials Session) : IRequest<VaultFile>;


using FluentVault.Domain;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFilePropertyDefinitionsCommand(
    List<long> MasterIds,
    List<long> AddedPropertyIds,
    List<long> RemovedPropertyIds,
    IEnumerable<string> Filenames,
    IEnumerable<string> AddedPropertyNames,
    IEnumerable<string> RemovedPropertyNames,
    VaultSessionCredentials Session) : IRequest<IEnumerable<VaultFile>>;

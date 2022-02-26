
using FluentVault.Domain;
using FluentVault.Domain.Search;

using MediatR;

namespace FluentVault.Features;

internal record SearchFilesCommand(
    IEnumerable<IDictionary<string, string>> SearchConditions,
    IEnumerable<IDictionary<string, string>> SortConditions,
    IEnumerable<long> FolderIds,
    bool RecurseFolders,
    bool LatestOnly,
    string Bookmark,
    VaultSessionCredentials Session) : IRequest<VaultFileSearchResult>;

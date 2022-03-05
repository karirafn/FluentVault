using FluentVault.Domain;
using FluentVault.Requests.Search.Files;

using MediatR;

namespace FluentVault.Requests.Search;

internal class SearchRequestBuilder : ISearchRequestBuilder
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    public SearchRequestBuilder(IMediator mediator, VaultSessionCredentials session)
        => (_mediator, _session) = (mediator, session);

    public ISearchFilesRequestBuilder Files => new SearchFilesRequestBuilder(_mediator, _session);
}

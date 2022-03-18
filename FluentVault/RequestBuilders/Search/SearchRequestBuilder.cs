using FluentVault.Requests.Search.Files;

using MediatR;

namespace FluentVault.Requests.Search;

internal class SearchRequestBuilder : ISearchRequestBuilder
{
    private readonly IMediator _mediator;

    public SearchRequestBuilder(IMediator mediator)
        => _mediator = mediator;

    public ISearchFilesRequestBuilder Files => new SearchFilesRequestBuilder(_mediator);
}


using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get.Files;
internal class GetFilesRequestBuilder : IRequestBuilder, IGetFilesRequestBuilder
{
    private readonly IMediator _mediator;

    public GetFilesRequestBuilder(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<VaultFile>> ByMasterIds(IEnumerable<VaultMasterId> masterIds) =>
        await _mediator.Send(new GetFilesByMasterIdsQuery(masterIds));
}

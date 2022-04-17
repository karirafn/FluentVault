using FluentVault.Features;
using FluentVault.RequestBuilders;

using MediatR;

namespace FluentVault.Requests.Update.File.LifecycleState;

internal class UpdateFileLifecycleStateRequestBuilder : IRequestBuilder, IUpdateFileLifecycleStateRequestBuilder, IWithFiles, IWithComment
{
    private readonly IMediator _mediator;

    private readonly List<VaultMasterId> _masterIds = new();
    private readonly List<string> _filenames = new();
    private readonly List<VaultLifeCycleStateId> _stateIds = new();

    public UpdateFileLifecycleStateRequestBuilder(IMediator mediator)
        => _mediator = mediator;

    public IWithFiles ByMasterId(VaultMasterId masterId) => ByMasterIds(new[] { masterId });
    public IWithFiles ByMasterIds(IEnumerable<VaultMasterId> masterIds)
    {
        _masterIds.AddRange(masterIds);
        return this;
    }

    public IWithFiles ByFilename(string filename) => ByFilenames(new[] { filename });
    public IWithFiles ByFilenames(IEnumerable<string> filenames)
    {
        _filenames.AddRange(filenames);
        return this;
    }

    public IWithComment ToStateWithId(VaultLifeCycleStateId stateId)
    {
        _stateIds.Add(stateId);
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        UpdateFileLifeCycleStatesCommand command = new(_filenames, _masterIds, _stateIds, comment);
        IEnumerable<VaultFile> response = await _mediator.Send(command);

        return response.Single();
    }

    public async Task<VaultFile> WithoutComment() => await WithComment(string.Empty);
}

using FluentVault.Domain;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Requests.Update.File.LifecycleState;

internal class UpdateFileLifecycleStateRequestBuilder : IUpdateFileLifecycleStateRequestBuilder, IWithFiles, IWithComment
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    private readonly List<MasterId> _masterIds = new();
    private readonly List<string> _filenames = new();
    private readonly List<LifeCycleStateId> _stateIds = new();
    private string _comment = string.Empty;

    public UpdateFileLifecycleStateRequestBuilder(IMediator mediator, VaultSessionCredentials session)
        => (_mediator, _session) = (mediator, session);

    public IWithFiles ByMasterId(MasterId masterId) => ByMasterIds(new[] { masterId });
    public IWithFiles ByMasterIds(IEnumerable<MasterId> masterIds)
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

    public IWithComment ToStateWithId(LifeCycleStateId stateId)
    {
        _stateIds.Add(stateId);
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        UpdateFileLifeCycleStateCommand command = new(_filenames, _masterIds, _stateIds, _comment, _session);
        VaultFile response = await _mediator.Send(command);

        return response;
    }

    public async Task<VaultFile> WithoutComment() => await WithComment(string.Empty);
}

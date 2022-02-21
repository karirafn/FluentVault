using FluentVault.Domain.Common;
using FluentVault.Requests.Update.File.LifecycleState;
using FluentVault.Requests.Update.File.PropertyDefinitions;

namespace FluentVault.Requests.Update;

internal class UpdateRequest : IUpdateRequest, IUpdateFileRequest
{
    private readonly VaultSession _session;

    public UpdateRequest(VaultSession session)
    {
        _session = session;
    }

    public IUpdateFileRequest File => this;
    public IUpdateFileLifecycleStateRequest LifecycleState => new UpdateFileLifecycleStateRequest(_session);
    public IUpdateFilePropertyDefinitionsRequest PropertyDefinitions => new UpdateFilePropertyDefinitionsRequest(_session);
}

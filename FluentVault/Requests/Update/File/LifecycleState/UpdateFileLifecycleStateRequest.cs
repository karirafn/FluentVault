using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;
using FluentVault.Domain.File;

namespace FluentVault.Requests.Update.File.LifecycleState;

internal class UpdateFileLifecycleStateRequest : SessionRequest, IUpdateFileLifecycleStateRequest, IWithFileMasterId, IWithComment
{
    private readonly List<long> _masterIds = new();
    private readonly List<long> _stateIds = new();
    private string _comment = string.Empty;

    public UpdateFileLifecycleStateRequest(VaultSession session)
        : base(session, RequestData.UpdateFileLifeCycleStates) { }

    public IWithFileMasterId WithMasterId(long masterId)
    {
        _masterIds.Add(masterId);
        return this;
    }

    public IWithComment ToStateWithId(long stateId)
    {
        _stateIds.Add(stateId);
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        _comment = comment;
        StringBuilder innerBody = GetInnerBody();
        XDocument document = await SendRequestAsync(innerBody);
        VaultFile file = document.ParseVaultFile();

        return file;
    }

    public async Task<VaultFile> WithoutComment() => await WithComment(string.Empty);

    private StringBuilder GetInnerBody()
        => new StringBuilder()
            .AppendElementWithAttribute(RequestData.Name, "xmlns", RequestData.Namespace)
            .AppendNestedElements("fileMasterIds", "long", _masterIds)
            .AppendNestedElements("toStateIds", "long", _stateIds)
            .AppendElement("comment", _comment)
            .AppendElementClosing(RequestData.Name);
}

using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;
using FluentVault.Domain.Common;
using FluentVault.Domain.File;
using FluentVault.Requests.Search.Files;

namespace FluentVault.Requests.Update.File.LifecycleState;

internal class UpdateFileLifecycleStateRequest : SessionRequest, IUpdateFileLifecycleStateRequest, IWithFiles, IWithComment
{
    private readonly List<long> _masterIds = new();
    private readonly List<string> _filenames = new();
    private readonly List<long> _stateIds = new();
    private string _comment = string.Empty;

    public UpdateFileLifecycleStateRequest(VaultSession session)
        : base(session, RequestData.UpdateFileLifeCycleStates) { }

    public IWithFiles ByMasterId(long masterId) => ByMasterIds(new[] { masterId });
    public IWithFiles ByMasterIds(IEnumerable<long> masterIds)
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

    public IWithComment ToStateWithId(long stateId)
    {
        _stateIds.Add(stateId);
        return this;
    }

    public async Task<VaultFile> WithComment(string comment)
    {
        if (_filenames.Any())
        {
            string searchString = string.Join(" OR ", _filenames);
            var result = await new SearchFilesRequest(Session)
                .ForValueEqualTo(searchString)
                .InSystemProperty(SearchStringProperty.FileName)
                .SearchWithoutPaging();

            _masterIds.AddRange(result.Select(x => x.MasterId));
        }

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

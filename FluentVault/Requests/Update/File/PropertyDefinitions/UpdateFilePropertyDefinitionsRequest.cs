using System.Text;
using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Common.Helpers;
using FluentVault.Domain.File;
using FluentVault.Requests.Get.Properties;
using FluentVault.Requests.Search.Files;

namespace FluentVault.Requests.Update.File.PropertyDefinitions;

internal class UpdateFilePropertyDefinitionsRequest : SessionRequest, IUpdateFilePropertyDefinitionsRequest, IUpdateFilePropertDefinitionsAction
{
    private readonly List<long> _masterIds = new();
    private readonly List<long> _addedPropertyIds = new();
    private readonly List<long> _removedPropertyIds = new();
    private List<string> _filenames = new();
    private List<string> _addedPropertyNames = new();
    private List<string> _removedPropertyNames = new();
    private IEnumerable<VaultProperty> _allProperties = new List<VaultProperty>();

    public UpdateFilePropertyDefinitionsRequest(VaultSession session)
        : base(session, RequestData.UpdateFilePropertyDefinitions) { }

    public async Task<IEnumerable<VaultFile>> ExecuteAsync()
    {
        if (_filenames.Any())
            await AddMasterIdsFromFilenames();

        if (_addedPropertyNames.Any())
            await AddAddedPropertyIdsFromPropertyNames();

        if (_removedPropertyNames.Any())
            await AddRemovedPropertyIdsFromPropertyNames();

        string innerBody = GetInnerBody();
        string requestBody = BodyBuilder.GetRequestBody(innerBody, Session.Ticket, Session.UserId);

        XDocument document = await SendAsync(requestBody);
        IEnumerable<VaultFile> files = document.ParseAllVaultFiles();

        return files;
    }

    public IUpdateFilePropertDefinitionsAction ByFileMasterId(long masterId) => ByFileMasterIds(new[] { masterId });
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<long> masterIds)
    {
        _masterIds.AddRange(masterIds);
        return this;
    }

    public IUpdateFilePropertDefinitionsAction ByFileName(string filename) => ByFileNames(new[] { filename });
    public IUpdateFilePropertDefinitionsAction ByFileNames(IEnumerable<string> filenames)
    {
        _filenames.AddRange(filenames);
        return this;
    }

    public IUpdateFilePropertDefinitionsAction AddPropertyById(long id) => AddPropertiesByIds(new[] { id });
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<long> ids)
    {
        _addedPropertyIds.AddRange(ids);
        return this;
    }

    public IUpdateFilePropertDefinitionsAction AddPropertyByName(string name) => AddPropertiesByNames(new[] { name });
    public IUpdateFilePropertDefinitionsAction AddPropertiesByNames(IEnumerable<string> names)
    {
        _addedPropertyNames.AddRange(names);
        return this;
    }

    public IUpdateFilePropertDefinitionsAction RemovePropertyById(long id) => RemovePropertiesByIds(new[] { id });
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<long> ids)
    {
        _removedPropertyIds.AddRange(ids);
        return this;
    }

    public IUpdateFilePropertDefinitionsAction RemovePropertyByName(string name) => RemovePropertiesByNames(new[] { name });
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByNames(IEnumerable<string> names)
    {
        _removedPropertyNames.AddRange(names);
        return this;
    }

    private string GetInnerBody() 
        => new StringBuilder()
            .AppendElementWithAttribute(RequestData.Name, "xmlns", RequestData.Namespace)
            .AppendNestedElementArray("masterIds", "long", _masterIds)
            .AppendNestedElementArray("addedPropDefIds", "long", _addedPropertyIds)
            .AppendNestedElementArray("removedPropDefIds", "long", _removedPropertyIds)
            .AppendElement("comment", "Add/Remove properties")
            .AppendClosingTag(RequestData.Name)
            .ToString();

    private async Task AddMasterIdsFromFilenames()
    {
        var searchString = string.Join(" OR ", _filenames);
        var files = await new SearchFilesRequest(Session)
            .ForValueEqualTo(searchString)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchWithoutPaging()
            ?? throw new Exception("Failed to search for filenames");

        _masterIds.AddRange(files.Select(x => x.MasterId));
        _filenames = new();
    }

    private async Task AddAddedPropertyIdsFromPropertyNames()
    {
        var ids = await GetPropertyIdsFromPropertyNames(_addedPropertyNames);
        _addedPropertyIds.AddRange(ids);
        _addedPropertyNames = new();
    }

    private async Task AddRemovedPropertyIdsFromPropertyNames()
    {
        var ids = await GetPropertyIdsFromPropertyNames(_removedPropertyNames);
        _removedPropertyIds.AddRange(ids);
        _removedPropertyNames = new();
    }

    private async Task<IEnumerable<long>> GetPropertyIdsFromPropertyNames(IEnumerable<string> names)
    {
        if (!_allProperties.Any())
            _allProperties = await new GetPropertiesRequest(Session).SendAsync();

        return _allProperties.Where(x => names.Contains(x.Definition.DisplayName))
               .Select(x => x.Definition.Id);
    }
}

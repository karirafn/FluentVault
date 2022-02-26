using FluentVault.Domain;
using FluentVault.Features;

using MediatR;

namespace FluentVault.Requests.Update.File.PropertyDefinitions;

internal class UpdateFilePropertyDefinitionsRequestBuilder : IUpdateFilePropertyDefinitionsRequestBuilder, IUpdateFilePropertDefinitionsAction
{
    private readonly IMediator _mediator;
    private readonly VaultSessionCredentials _session;

    private readonly List<long> _masterIds = new();
    private readonly List<long> _addedPropertyIds = new();
    private readonly List<long> _removedPropertyIds = new();
    private readonly List<string> _filenames = new();
    private readonly List<string> _addedPropertyNames = new();
    private readonly List<string> _removedPropertyNames = new();

    public UpdateFilePropertyDefinitionsRequestBuilder(IMediator mediator, VaultSessionCredentials session)
    {
        _mediator = mediator;
        _session = session;
    }

    public async Task<IEnumerable<VaultFile>> ExecuteAsync()
    {
        var command = new UpdateFilePropertyDefinitionsCommand(_masterIds, _addedPropertyIds, _removedPropertyIds, _filenames, _addedPropertyNames, _removedPropertyNames, _session);
        var files = await _mediator.Send(command);
        
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
}

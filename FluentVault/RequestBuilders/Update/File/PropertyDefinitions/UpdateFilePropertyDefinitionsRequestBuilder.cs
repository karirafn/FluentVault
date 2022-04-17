using FluentVault.Features;
using FluentVault.RequestBuilders;

using MediatR;

namespace FluentVault.Requests.Update.File.PropertyDefinitions;

internal class UpdateFilePropertyDefinitionsRequestBuilder : IRequestBuilder, IUpdateFilePropertyDefinitionsRequestBuilder, IUpdateFilePropertDefinitionsAction
{
    private readonly IMediator _mediator;

    private readonly List<VaultMasterId> _masterIds = new();
    private readonly List<VaultPropertyDefinitionId> _addedPropertyIds = new();
    private readonly List<VaultPropertyDefinitionId> _removedPropertyIds = new();
    private readonly List<string> _filenames = new();
    private readonly List<string> _addedPropertyNames = new();
    private readonly List<string> _removedPropertyNames = new();

    public UpdateFilePropertyDefinitionsRequestBuilder(IMediator mediator)
        => _mediator = mediator;

    public async Task<IEnumerable<VaultFile>> ExecuteAsync()
    {
        var command = new UpdateFilePropertyDefinitionsCommand(_masterIds, _addedPropertyIds, _removedPropertyIds, _filenames, _addedPropertyNames, _removedPropertyNames);
        var files = await _mediator.Send(command);
        
        return files;
    }

    public IUpdateFilePropertDefinitionsAction ByFileMasterId(VaultMasterId masterId) => ByFileMasterIds(new[] { masterId });
    public IUpdateFilePropertDefinitionsAction ByFileMasterIds(IEnumerable<VaultMasterId> masterIds)
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

    public IUpdateFilePropertDefinitionsAction AddPropertyById(VaultPropertyDefinitionId id) => AddPropertiesByIds(new[] { id });
    public IUpdateFilePropertDefinitionsAction AddPropertiesByIds(IEnumerable<VaultPropertyDefinitionId> ids)
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

    public IUpdateFilePropertDefinitionsAction RemovePropertyById(VaultPropertyDefinitionId id) => RemovePropertiesByIds(new[] { id });
    public IUpdateFilePropertDefinitionsAction RemovePropertiesByIds(IEnumerable<VaultPropertyDefinitionId> ids)
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

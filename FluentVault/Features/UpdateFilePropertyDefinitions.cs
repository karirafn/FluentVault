using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFilePropertyDefinitionsCommand(
    List<VaultMasterId> MasterIds,
    List<VaultPropertyDefinitionId> AddedPropertyIds,
    List<VaultPropertyDefinitionId> RemovedPropertyIds,
    IEnumerable<string>? Filenames = null,
    IEnumerable<string>? AddedPropertyNames = null,
    IEnumerable<string>? RemovedPropertyNames = null) : IRequest<IEnumerable<VaultFile>>;

internal class UpdateFilePropertyDefinitionsHandler : IRequestHandler<UpdateFilePropertyDefinitionsCommand, IEnumerable<VaultFile>>
{
    private const string Operation = "UpdateFilePropertyDefinitions";

    private readonly IMediator _mediator;
    private readonly IVaultService _vaultRequestService;
    private IEnumerable<VaultProperty> _allProperties = Enumerable.Empty<VaultProperty>();

    public UpdateFilePropertyDefinitionsHandler(IMediator mediator, IVaultService vaultRequestService)
        => (_mediator, _vaultRequestService) = (mediator, vaultRequestService);

    public async Task<IEnumerable<VaultFile>> Handle(UpdateFilePropertyDefinitionsCommand command, CancellationToken cancellationToken)
    {
        if (CanHandle(command) is false)
            return Enumerable.Empty<VaultFile>();

        await AddMasterIdsFromFilenames(command);
        await AddAddedPropertyIdsFromPropertyNames(command);
        await AddRemovedPropertyIdsFromPropertyNames(command);

        void contentBuilder(XElement content, XNamespace ns) => content
            .AddNestedElements(ns, "masterIds", "long", command.MasterIds)
            .AddNestedElements(ns, "addedPropDefIds", "long", command.AddedPropertyIds)
            .AddNestedElements(ns, "removedPropDefIds", "long", command.RemovedPropertyIds)
            .AddElement(ns, "comment", "Add/Remove properties");

        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        IEnumerable<VaultFile> files = new UpdateFilePropertyDefinitionsSerializer().DeserializeMany(response);

        return files;
    }

    private static bool CanHandle(UpdateFilePropertyDefinitionsCommand command) => new bool[]
        {
            command.MasterIds.Any() || (command.Filenames?.Any() ?? false),
            command.AddedPropertyIds.Any() || command.RemovedPropertyIds.Any() || (command.AddedPropertyNames?.Any() ?? false) || (command.RemovedPropertyNames?.Any() ?? false)
        }.All(x => x);

    private async Task AddMasterIdsFromFilenames(UpdateFilePropertyDefinitionsCommand command)
    {
        if ((command.Filenames?.Any() ?? false) is false)
            return;

        SearchCondition searchCondition = new(
            StringSearchProperty.FileName,
            SearchOperator.Contains,
            string.Join(" OR ", command.Filenames),
            SearchPropertyType.SingleProperty,
            SearchRule.Must);
        FindFilesBySearchConditionsQuery query = new(new[] { searchCondition.Attributes });
        VaultSearchFilesResponse response = await _mediator.Send(query);
        IEnumerable<VaultMasterId> masterIds = response.Result.Files
            .Select(file => file.MasterId)
            .Where(id => command.MasterIds.Contains(id) is false);

        command.MasterIds.AddRange(masterIds);
    }

    private async Task AddAddedPropertyIdsFromPropertyNames(UpdateFilePropertyDefinitionsCommand command)
    {
        if ((command.AddedPropertyNames?.Any() ?? false) is false)
            return;

        IEnumerable<VaultPropertyDefinitionId> ids = await GetPropertyIdsFromPropertyNames(command.AddedPropertyNames);

        command.AddedPropertyIds.AddRange(ids.Where(id => command.AddedPropertyIds.Contains(id) is false));
    }

    private async Task AddRemovedPropertyIdsFromPropertyNames(UpdateFilePropertyDefinitionsCommand command)
    {
        if ((command.RemovedPropertyNames?.Any() ?? false) is false)
            return;

        IEnumerable<VaultPropertyDefinitionId> ids = await GetPropertyIdsFromPropertyNames(command.AddedPropertyNames);

        command.RemovedPropertyIds.AddRange(ids.Where(id => command.RemovedPropertyIds.Contains(id) is false));
    }

    private async Task<IEnumerable<VaultPropertyDefinitionId>> GetPropertyIdsFromPropertyNames(IEnumerable<string>? names)
    {
        if (names is null)
            return Enumerable.Empty<VaultPropertyDefinitionId>();

        _allProperties = _allProperties.Any()
            ? _allProperties
            : await _mediator.Send(new GetAllPropertyDefinitionInfosQuery());

        return _allProperties
            .Where(property => names.Contains(property.Definition.DisplayName))
            .Select(property => property.Definition.Id);
    }
}

internal class UpdateFilePropertyDefinitionsSerializer : XDocumentSerializer<VaultFile>
{
    private const string UpdateFilePropertyDefinitions = nameof(UpdateFilePropertyDefinitions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(UpdateFilePropertyDefinitions);

    public UpdateFilePropertyDefinitionsSerializer() : base(_request.Operation, new VaultFileSerializer(_request.Namespace)) { }
}

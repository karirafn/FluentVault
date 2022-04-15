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
    IEnumerable<string> Filenames,
    IEnumerable<string> AddedPropertyNames,
    IEnumerable<string> RemovedPropertyNames) : IRequest<IEnumerable<VaultFile>>;

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
        if (command.Filenames.Any())
            command.MasterIds.AddRange(await GetMasterIdsFromFilenames(command));

        if (command.AddedPropertyNames.Any())
            command.AddedPropertyIds.AddRange(await GetPropertyIdsFromPropertyNames(command.AddedPropertyNames));

        if (command.RemovedPropertyNames.Any())
            command.AddedPropertyIds.AddRange(await GetPropertyIdsFromPropertyNames(command.RemovedPropertyNames));

        void contentBuilder(XElement content, XNamespace ns)
            => content.AddNestedElements(ns, "masterIds", "long", command.MasterIds)
                .AddNestedElements(ns, "addedPropDefIds", "long", command.AddedPropertyIds)
                .AddNestedElements(ns, "removedPropDefIds", "long", command.RemovedPropertyIds)
                .AddElement(ns, "comment", "Add/Remove properties");

        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        IEnumerable<VaultFile> files = new UpdateFilePropertyDefinitionsSerializer().DeserializeMany(response);

        return files;
    }

    private async Task<IEnumerable<VaultMasterId>> GetMasterIdsFromFilenames(UpdateFilePropertyDefinitionsCommand command)
    {
        SearchCondition searchCondition = new(
            StringSearchProperty.FileName,
            SearchOperator.Contains,
            string.Join(" OR ", command.Filenames),
            SearchPropertyType.SingleProperty,
            SearchRule.Must);
        FindFilesBySearchConditionsQuery query = new(new[] { searchCondition.Attributes });
        VaultSearchFilesResponse response = await _mediator.Send(query);
        IEnumerable<VaultMasterId> masterIds = response.Result.Files.Select(file => file.MasterId);

        return masterIds;
    }

    private async Task<IEnumerable<VaultPropertyDefinitionId>> GetPropertyIdsFromPropertyNames(IEnumerable<string> names)
    {
        _allProperties = _allProperties.Any()
            ? _allProperties
            : await _mediator.Send(new GetAllPropertyDefinitionInfosQuery());

        return _allProperties
            .Where(x => names.Contains(x.Definition.DisplayName))
            .Select(x => x.Definition.Id);
    }
}

internal class UpdateFilePropertyDefinitionsSerializer : XDocumentSerializer<VaultFile>
{
    private const string UpdateFilePropertyDefinitions = nameof(UpdateFilePropertyDefinitions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(UpdateFilePropertyDefinitions);

    public UpdateFilePropertyDefinitionsSerializer() : base(_request.Operation, new VaultFileSerializer(_request.Namespace)) { }
}

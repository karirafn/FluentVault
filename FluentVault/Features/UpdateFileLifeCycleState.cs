using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Domain.Search;
using FluentVault.Extensions;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFileLifeCycleStatesCommand(
    IEnumerable<string> FileNames,
    IEnumerable<VaultMasterId> MasterIds,
    IEnumerable<VaultLifeCycleStateId> StateIds,
    string Comment) : IRequest<IEnumerable<VaultFile>>;

internal class UpdateFileLifeCycleStatesHandler : IRequestHandler<UpdateFileLifeCycleStatesCommand, IEnumerable<VaultFile>>
{
    private const string Operation = "UpdateFileLifeCycleStates";

    private readonly IMediator _mediator;
    private readonly IVaultService _vaultRequestService;

    public UpdateFileLifeCycleStatesHandler(IMediator mediator, IVaultService soapRequestService)
        => (_mediator, _vaultRequestService) = (mediator, soapRequestService);

    public async Task<IEnumerable<VaultFile>> Handle(UpdateFileLifeCycleStatesCommand command, CancellationToken cancellationToken)
    {
        command = command.FileNames.Any()
            ? command with { MasterIds = await GetMasterIdsFromFilenames(command, cancellationToken) }
            : command;

        void contentBuilder(XElement content, XNamespace ns)
            => content.AddNestedElements(ns, "fileMasterIds", "long", command.MasterIds.Select(x => x.ToString()))
                .AddNestedElements(ns, "toStateIds", "long", command.StateIds.Select(x => x.ToString()))
                .AddElement(ns, "comment", command.Comment);

        XDocument document = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        IEnumerable<VaultFile> files = new UpdateFileLifeCycleStatesSerializer().DeserializeMany(document);

        return files;
    }

    private async Task<List<VaultMasterId>> GetMasterIdsFromFilenames(UpdateFileLifeCycleStatesCommand command, CancellationToken cancellationToken)
    {
        List<VaultMasterId> masterIds = command.MasterIds.ToList();
        SearchCondition searchCondition = new(
            StringSearchProperty.FileName.Value,
            SearchOperator.Contains,
            string.Join(" OR ", command.FileNames),
            SearchPropertyType.SingleProperty,
            SearchRule.Must);
        FindFilesBySearchConditionsQuery query = new(new[] { searchCondition.Attributes });
        VaultSearchFilesResponse response = await _mediator.Send(query, cancellationToken);

        masterIds.AddRange(response.Result.Files.Select(x => x.MasterId));

        return masterIds;
    }
}

internal class UpdateFileLifeCycleStatesSerializer : XDocumentSerializer<VaultFile>
{
    private const string UpdateFileLifeCycleStates = nameof(UpdateFileLifeCycleStates);
    private static readonly VaultRequest _request = new VaultRequestData().Get(UpdateFileLifeCycleStates);

    public UpdateFileLifeCycleStatesSerializer() : base(_request.Operation, new VaultFileSerializer(_request.Namespace)) { }
}

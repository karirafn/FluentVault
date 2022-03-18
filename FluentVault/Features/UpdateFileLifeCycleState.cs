using System.Xml.Linq;

using FluentVault.Common;
using FluentVault.Extensions;
using FluentVault.Requests.Search.Files;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFileLifeCycleStateCommand(
    IEnumerable<string> FileNames,
    IEnumerable<VaultMasterId> MasterIds,
    IEnumerable<VaultLifeCycleStateId> StateIds,
    string Comment) : IRequest<VaultFile>;


internal class UpdateFileLifeCycleStateHandler : IRequestHandler<UpdateFileLifeCycleStateCommand, VaultFile>
{
    private const string Operation = "UpdateFileLifeCycleStates";

    private readonly IMediator _mediator;
    private readonly IVaultService _vaultRequestService;

    public UpdateFileLifeCycleStateHandler(IMediator mediator, IVaultService soapRequestService)
        => (_mediator, _vaultRequestService) = (mediator, soapRequestService);

    public async Task<VaultFile> Handle(UpdateFileLifeCycleStateCommand command, CancellationToken cancellationToken)
    {
        List<VaultMasterId> masterIds = command.MasterIds.ToList();
        if (command.FileNames.Any())
        {
            string searchString = string.Join(" OR ", command.FileNames);
            var response = await new SearchFilesRequestBuilder(_mediator)
                .ForValueEqualTo(searchString)
                .InSystemProperty(StringSearchProperty.FileName)
                .WithoutPaging();

            masterIds.AddRange(response.Select(x => x.MasterId));
            command = command with { MasterIds = masterIds };
        }

        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddNestedElements(ns, "fileMasterIds", "long", command.MasterIds.Select(x => x.ToString()));
            content.AddNestedElements(ns, "toStateIds", "long", command.StateIds.Select(x => x.ToString()));
            content.AddElement(ns, "comment", command.Comment);
        };

        XDocument document = await _vaultRequestService.SendAsync(Operation, canSignIn: true, contentBuilder, cancellationToken);
        VaultFile file = VaultFile.ParseSingle(document);

        return file;
    }
}

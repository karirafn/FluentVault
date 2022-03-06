using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Domain.SOAP;
using FluentVault.Requests.Search.Files;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFileLifeCycleStateCommand(
    IEnumerable<string> FileNames,
    IEnumerable<long> MasterIds,
    IEnumerable<long> StateIds,
    string Comment,
    VaultSessionCredentials Session) : IRequest<VaultFile>;


internal class UpdateFileLifeCycleStateHandler : IRequestHandler<UpdateFileLifeCycleStateCommand, VaultFile>
{
    private const string Operation = "UpdateFileLifeCycleStates";

    private readonly IMediator _mediator;
    private readonly ISoapRequestService _soapRequestService;

    public UpdateFileLifeCycleStateHandler(IMediator mediator, ISoapRequestService soapRequestService)
        => (_mediator, _soapRequestService) = (mediator, soapRequestService);

    public async Task<VaultFile> Handle(UpdateFileLifeCycleStateCommand command, CancellationToken cancellationToken)
    {
        List<long> masterIds = command.MasterIds.ToList();
        if (command.FileNames.Any())
        {
            string searchString = string.Join(" OR ", command.FileNames);
            var response = await new SearchFilesRequestBuilder(_mediator, command.Session)
                .ForValueEqualTo(searchString)
                .InSystemProperty(SearchStringProperty.FileName)
                .SearchWithoutPaging();

            masterIds.AddRange(response.Select(x => x.MasterId));
            command = command with { MasterIds = masterIds };
        }

        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddNestedElements(ns, "fileMasterIds", "long", command.MasterIds.Select(x => x.ToString()));
            content.AddNestedElements(ns, "toStateIds", "long", command.StateIds.Select(x => x.ToString()));
            content.AddElement(ns, "comment", command.Comment);
        };

        XDocument document = await _soapRequestService.SendAsync(Operation, command.Session, contentBuilder);
        VaultFile file = VaultFile.ParseSingle(document);

        return file;
    }
}

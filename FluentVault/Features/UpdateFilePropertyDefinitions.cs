using System.Xml.Linq;

using FluentVault.Common.Extensions;
using FluentVault.Domain.SOAP;
using FluentVault.Requests.Search.Files;

using MediatR;

namespace FluentVault.Features;

internal record UpdateFilePropertyDefinitionsCommand(
    List<long> MasterIds,
    List<long> AddedPropertyIds,
    List<long> RemovedPropertyIds,
    IEnumerable<string> Filenames,
    IEnumerable<string> AddedPropertyNames,
    IEnumerable<string> RemovedPropertyNames,
    VaultSessionCredentials Session) : IRequest<IEnumerable<VaultFile>>;

internal class UpdateFilePropertyDefinitionsHandler : IRequestHandler<UpdateFilePropertyDefinitionsCommand, IEnumerable<VaultFile>>
{
    private const string Operation = "UpdateFilePropertyDefinitions";

    private readonly IMediator _mediator;
    private readonly ISoapRequestService _soapRequestService;
    private IEnumerable<VaultPropertyDefinition> _allProperties = new List<VaultPropertyDefinition>();

    public UpdateFilePropertyDefinitionsHandler(IMediator mediator, ISoapRequestService soapRequestService)
        => (_mediator, _soapRequestService) = (mediator, soapRequestService);

    public async Task<IEnumerable<VaultFile>> Handle(UpdateFilePropertyDefinitionsCommand command, CancellationToken cancellationToken)
    {
        if (command.Filenames.Any())
            command.MasterIds.AddRange(await GetMasterIdsFromFilenames(command));

        if (command.AddedPropertyNames.Any())
            command.AddedPropertyIds.AddRange(await GetPropertyIdsFromPropertyNames(command.AddedPropertyNames, command));

        if (command.RemovedPropertyNames.Any())
            command.AddedPropertyIds.AddRange(await GetPropertyIdsFromPropertyNames(command.RemovedPropertyNames, command));

        void contentBuilder(XElement content, XNamespace ns)
        {
            content.AddNestedElements(ns, "masterIds", "long", command.MasterIds.Select(x => x.ToString()));
            content.AddNestedElements(ns, "addedPropDefIds", "long", command.AddedPropertyIds.Select(x => x.ToString()));
            content.AddNestedElements(ns, "removedPropDefIds", "long", command.RemovedPropertyIds.Select(x => x.ToString()));
            content.AddElement(ns, "comment", "Add/Remove properties");
        };

        XDocument response = await _soapRequestService.SendAsync(Operation, command.Session, contentBuilder);
        var files = VaultFile.ParseAll(response);

        return files;
    }

    private async Task<IEnumerable<long>> GetMasterIdsFromFilenames(UpdateFilePropertyDefinitionsCommand command)
    {
        var searchString = string.Join(" OR ", command.Filenames);
        var files = await new SearchFilesRequestBuilder(_mediator, command.Session)
            .ForValueEqualTo(searchString)
            .InSystemProperty(SearchStringProperty.FileName)
            .SearchWithoutPaging()
            ?? throw new Exception("Failed to search for filenames");

        var masterIds = files.Select(x => x.MasterId);

        return masterIds;
    }

    private async Task<IEnumerable<long>> GetPropertyIdsFromPropertyNames(IEnumerable<string> names, UpdateFilePropertyDefinitionsCommand command)
    {
        if (!_allProperties.Any())
            _allProperties = await _mediator.Send(new GetPropertyDefinitionsQuery(command.Session));

        return _allProperties.Where(x => names.Contains(x.Definition.DisplayName))
               .Select(x => x.Definition.Id);
    }
}

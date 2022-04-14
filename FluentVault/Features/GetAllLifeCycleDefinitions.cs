using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllLifeCycleDefinitionsQuery() : IRequest<IEnumerable<VaultLifeCycleDefinition>>;

internal class GetAllLifeCycleDefinitionsHandler : IRequestHandler<GetAllLifeCycleDefinitionsQuery, IEnumerable<VaultLifeCycleDefinition>>
{
    private const string Operation = "GetAllLifeCycleDefinitions";

    private readonly IVaultService _vaultRequestService;

    public GetAllLifeCycleDefinitionsHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultLifeCycleDefinition>> Handle(GetAllLifeCycleDefinitionsQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, cancellationToken: cancellationToken);
        IEnumerable<VaultLifeCycleDefinition> lifeCycles = new GetAllLifeCycleDefinitionsSerializer().DeserializeMany(response);

        return lifeCycles;
    }
}

internal class GetAllLifeCycleDefinitionsSerializer : XDocumentSerializer<VaultLifeCycleDefinition>
{
    private const string GetAllLifeCycleDefinitions = nameof(GetAllLifeCycleDefinitions);
    private static readonly VaultRequest _request = new VaultRequestData().Get(GetAllLifeCycleDefinitions);

    public GetAllLifeCycleDefinitionsSerializer() : base(_request.Operation, new VaultLifeCycleDefinitionSerializer(_request.Namespace)) { }
}

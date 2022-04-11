﻿using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetAllPropertyDefinitionInfosQuery() : IRequest<IEnumerable<VaultProperty>>;

internal class GetAllPropertyDefinitionInfosHandler : IRequestHandler<GetAllPropertyDefinitionInfosQuery, IEnumerable<VaultProperty>>
{
    private const string Operation = "GetPropertyDefinitionInfosByEntityClassId";

    private readonly IVaultService _vaultRequestService;

    public GetAllPropertyDefinitionInfosHandler(IVaultService vaultRequestService)
        => _vaultRequestService = vaultRequestService;

    public async Task<IEnumerable<VaultProperty>> Handle(GetAllPropertyDefinitionInfosQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _vaultRequestService.SendAsync(Operation, canSignIn: true, cancellationToken: cancellationToken);
        IEnumerable<VaultProperty> properties = VaultProperty.ParseAll(response);

        return properties;
    }
}
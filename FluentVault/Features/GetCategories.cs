﻿using System.Xml.Linq;

using FluentVault.Common;

using MediatR;

namespace FluentVault.Features;

internal record GetCategoriesQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultCategory>>;

internal class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<VaultCategory>>
{
    private const string Operation = "GetCategoryConfigurationsByBehaviorNames";

    private readonly IVaultRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetCategoriesHandler(IVaultRequestService soapRequestService, VaultSessionCredentials session)
        => (_soapRequestService, _session) = (soapRequestService, session);

    public async Task<IEnumerable<VaultCategory>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultCategory> categories = VaultCategory.ParseAll(response);

        return categories;
    }
}

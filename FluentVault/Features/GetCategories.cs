﻿using System.Xml.Linq;

using FluentVault.Domain;
using FluentVault.Domain.Category;
using FluentVault.Domain.SOAP;

using MediatR;

namespace FluentVault.Features;

public record GetCategoriesQuery(VaultSessionCredentials Session) : IRequest<IEnumerable<VaultCategory>>;

internal class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<VaultCategory>>
{
    private const string Operation = "GetCategoryConfigurationsByBehaviorNames";

    private readonly ISoapRequestService _soapRequestService;
    private readonly VaultSessionCredentials _session;

    public GetCategoriesHandler(ISoapRequestService soapRequestService, VaultSessionCredentials session)
    {
        _soapRequestService = soapRequestService;
        _session = session;
    }

    public async Task<IEnumerable<VaultCategory>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        XDocument response = await _soapRequestService.SendAsync(Operation, _session);
        IEnumerable<VaultCategory> categories = response.ParseCategories();

        return categories;
    }
}

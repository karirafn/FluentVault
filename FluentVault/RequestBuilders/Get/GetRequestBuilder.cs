﻿using FluentVault.Features;

using MediatR;

namespace FluentVault.RequestBuilders.Get;
internal class GetRequestBuilder : IRequestBuilder, IGetRequestBuilder
{
    private readonly IMediator _mediator;

    public GetRequestBuilder(
        IMediator mediator,
        IGetClientShortcutRequestBuilder clientShortcut,
        IGetFilesRequestBuilder files,
        IGetLatestRequestBuilder latest,
        IGetPropertiesRequestBuilder properties,
        IGetRevisionRequestBuilder revision
        )
    {
        _mediator = mediator;
        Properties = properties;
        Latest = latest;
        Revision = revision;
        ClientShortcut = clientShortcut;
        Files = files;
    }

    public IGetClientShortcutRequestBuilder ClientShortcut { get; }
    public IGetFilesRequestBuilder Files { get; }
    public IGetLatestRequestBuilder Latest { get; }
    public IGetPropertiesRequestBuilder Properties { get; }
    public IGetRevisionRequestBuilder Revision { get; }

    public async Task<IEnumerable<VaultCategory>> CategoryConfigurations(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllCategoryConfigurationsQuery(), cancellationToken);

    public async Task<IEnumerable<VaultLifeCycleDefinition>> LifeCycleDefinitions(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllLifeCycleDefinitionsQuery(), cancellationToken);

    public async Task<IEnumerable<VaultProperty>> PropertyDefinitionInfos(CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetAllPropertyDefinitionInfosQuery(), cancellationToken);

    public async Task<IEnumerable<VaultUserInfo>> UserInfos(IEnumerable<VaultUserId> ids, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetUserInfosByIserIdsQuery(ids), cancellationToken);

    public async Task<VaultFile> LatestFileByMasterId(VaultMasterId id, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetLatestFileByMasterIdQuery(id), cancellationToken);

    public async Task<VaultItem> LatestItemByMasterId(VaultMasterId id, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetLatestItemByItemMasterIdQuery(id), cancellationToken);

    public async Task<IEnumerable<VaultFolder>> FoldersByFileMasterIds(IEnumerable<VaultMasterId> masterIds, CancellationToken cancellationToken = default)
        => await _mediator.Send(new GetFoldersByFileMasterIdsQuery(masterIds), cancellationToken);
}

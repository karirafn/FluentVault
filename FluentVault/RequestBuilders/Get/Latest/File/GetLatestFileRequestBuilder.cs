namespace FluentVault.RequestBuilders.Get.Latest.File;
internal class GetLatestFileRequestBuilder : IRequestBuilder, IGetLatestFileRequestBuilder
{
    public GetLatestFileRequestBuilder(IGetLatestFileAssociationsRequestBuilder associations)
    {
        Associations = associations;
    }

    public IGetLatestFileAssociationsRequestBuilder Associations { get; }
}

namespace FluentVault.RequestBuilders.Get.Revision.File;
internal class GetRevisionFileRequestBuilder : IRequestBuilder, IGetRevisionFileRequestBuilder
{
    public GetRevisionFileRequestBuilder(IGetRevisionFileAssociationsRequestBuilder associations)
    {
        Associations = associations;
    }

    public IGetRevisionFileAssociationsRequestBuilder Associations { get; }
}

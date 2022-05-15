namespace FluentVault.RequestBuilders.Get.Latest;
internal class GetLatestRequestBuilder : IRequestBuilder, IGetLatestRequestBuilder
{
    public GetLatestRequestBuilder(IGetLatestFileRequestBuilder file)
    {
        File = file;
    }

    public IGetLatestFileRequestBuilder File { get; }
}

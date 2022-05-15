namespace FluentVault.RequestBuilders.Get.Revision;
internal class GetRevisionRequestBuilder : IRequestBuilder, IGetRevisionRequestBuilder
{
    public GetRevisionRequestBuilder(IGetRevisionFileRequestBuilder file)
    {
        File = file;
    }

    public IGetRevisionFileRequestBuilder File { get; }
}
